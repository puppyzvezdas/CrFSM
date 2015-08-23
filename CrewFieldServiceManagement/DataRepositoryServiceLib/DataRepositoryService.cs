using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Common.model;
using Common;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Xml.Serialization;
using Polenter.Serialization;
using System.ServiceModel;
using Common.Model;
using Common.PubSubCommon;
using System.Configuration;

namespace DataRepositoryServiceLib
{

    public class DataRepositoryService : IDataRepositoryService
    {
        private string path = Common.Config.DataPath;

        private IPublish proxy;

        public DataRepositoryService()
        {
            CrFSMLogger.CrFSMLogger.Instance.WriteToLog("Creating data repositoty service");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            Directory.SetCurrentDirectory(path);
   
                CreateProxy();
        }

        private void CreateProxy()
        {
            try
            {
                EndpointAddress endpointAddres = new EndpointAddress(ConfigurationManager.AppSettings["EndpointAddress"]);
                NetTcpBinding binding = new NetTcpBinding();
                TimeSpan ts = new TimeSpan(1, 0, 0);
                binding.CloseTimeout = ts;
                binding.OpenTimeout = ts;
                binding.SendTimeout = ts;
                binding.ReceiveTimeout = ts;
                proxy = ChannelFactory<IPublish>.CreateChannel(binding, endpointAddres);
            }
            catch (Exception ex)
            {
                CrFSMLogger.CrFSMLogger.Instance.WriteToLog(ex);
            }
        }

        private void SendEvent(Message data, string topicName)
        {
            CrFSMLogger.CrFSMLogger.Instance.WriteToLog(string.Format("'Sending event to subsribers of \"{0}\" topic", topicName));
            proxy.Publish(data, topicName);
            CrFSMLogger.CrFSMLogger.Instance.WriteToLog(string.Format("Event sent to subsribers of \"{0}\" topic", topicName));
        }

        public void AddEntity(IdentifiedObject _entity)
        {
            string fileName = _entity.Type.ToString() + ".xml";
            bool fileExist = true;
            if (!File.Exists(fileName))
            {
                CrFSMLogger.CrFSMLogger.Instance.WriteToLog(string.Format("Creating new data file {0}", path + fileName));
                File.Create(fileName).Close();
                fileExist = false;
            }

            Hashtable idObjCollXml = new Hashtable();

            if (fileExist)
            {
                SharpDeserialization(_entity.Type, ref idObjCollXml);
            }

            idObjCollXml.Add(_entity.GlobalId, _entity);
            SharpSerialization(_entity.Type, ref idObjCollXml);

            Message data = new Message();
            data.TopicName = _entity.Type.ToString();
            data.IdObjCollection.AddRange(idObjCollXml.Values.Cast<IdentifiedObject>().ToList());
            SendEvent(data, data.TopicName);
        }

        public IdentifiedObject GetEntityById(Common.EntityType _type, string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                CrFSMLogger.CrFSMLogger.Instance.WriteToLog(string.Format("Entity of type {0} is unavalible because id is null", _type));
                throw new FaultException();
            }
            CrFSMLogger.CrFSMLogger.Instance.WriteToLog(string.Format("Getting entity of type {0} and ID {1}", _type, id));
            Hashtable idObjCollection = new Hashtable();
            SharpDeserialization(_type, ref idObjCollection);
            return idObjCollection[id] as IdentifiedObject;
        }

        public void RemoveEntity(IdentifiedObject _entity)
        {
            try
            {
                CrFSMLogger.CrFSMLogger.Instance.WriteToLog(string.Format("Trying to remove entity {0}", _entity));
                Hashtable idObjCollection = new Hashtable();
                SharpDeserialization(_entity.Type, ref idObjCollection);
                idObjCollection.Remove(_entity.GlobalId);
                SharpSerialization(_entity.Type, ref idObjCollection);
                CrFSMLogger.CrFSMLogger.Instance.WriteToLog(string.Format("Entity {0} is successfully removed", _entity));

                Message data = new Message();
                data.TopicName = _entity.Type.ToString();
                data.IdObjCollection.AddRange(idObjCollection.Values.Cast<IdentifiedObject>().ToList());
                SendEvent(data, data.TopicName);
            }
            catch (FaultException ex)
            {
                CrFSMLogger.CrFSMLogger.Instance.WriteToLog(ex);
                throw ex;
            }
        }

        public List<IdentifiedObject> GetEntityByType(EntityType _type)
        {
            Hashtable idObjCollection = new Hashtable();
            string fileName = _type.ToString() + ".dat";
            try
            {
                CrFSMLogger.CrFSMLogger.Instance.WriteToLog(string.Format("Getting data of type {0} from datafile {1}", _type, fileName));
                SharpDeserialization(_type, ref idObjCollection);
            }
            catch (Exception ex)
            {
                CrFSMLogger.CrFSMLogger.Instance.WriteToLog(string.Format("ERROR occurred: {0}", ex));
            }
            return idObjCollection.Values.Cast<IdentifiedObject>().ToList();
        }

        private void SharpSerialization(EntityType _type, ref Hashtable _idObjCollection)
        {
            string fileName = _type.ToString() + ".xml";

            SharpSerializer serializer = new SharpSerializer();
            serializer.Serialize(_idObjCollection, fileName);

            if (_type == EntityType.CrewMember)
            {
                fileName = EntityType.User.ToString() + ".xml";
                Hashtable temp = (Hashtable)serializer.Deserialize("User.xml");
                Hashtable temp2 = new Hashtable((from item in _idObjCollection.Values.Cast<CrewMember>()
                                                 select new User(item.Username, item.Password, item.UserType)).ToDictionary(obj => obj.Username, obj => obj));
                temp2.Add("admin", temp["admin"]);
                serializer.Serialize(temp2, fileName);
            }
        }

        private void SharpDeserialization(EntityType _type, ref Hashtable idObjCollection)
        {
            string fileName = _type.ToString() + ".xml";

            SharpSerializer serializer = new SharpSerializer();
            idObjCollection = (Hashtable)serializer.Deserialize(fileName);
        }


        public User GetUserByUsername(string _username)
        {

            Hashtable idObjCollXml = new Hashtable();
            SharpDeserialization(EntityType.User, ref idObjCollXml);
            var retVal = idObjCollXml[_username];

            if (retVal == null)
            {
                throw new FaultException("Unknown username");
            }
            return retVal as User;
        }


        public void UpdateEntity(IdentifiedObject _entity)
        {
            Hashtable idObjCollection = new Hashtable();

            SharpDeserialization(_entity.Type, ref idObjCollection);
            idObjCollection[_entity.GlobalId] = _entity;
            SharpSerialization(_entity.Type, ref idObjCollection);
        }


        public int CreateWorkingDays()
        {
            DateTime d = new DateTime(2015, 1, 1);
            int brojac = 0;
            Hashtable ht = new Hashtable();
            while (d.Year < 2016)
            {
                WorkingDay wd = new WorkingDay(d);
                ht.Add(wd.ConcreteDay, wd);
                d = d.Date.AddDays(1);
                ++brojac;
            }
            SharpSerialization(EntityType.WorkingDay, ref ht);
            return brojac;
        }


        public WorkingDay GetDayOfYear(DateTime _day)
        {
            Hashtable idObjCollection = new Hashtable();
            SharpDeserialization(EntityType.WorkingDay, ref idObjCollection);
            return idObjCollection[_day] as WorkingDay;
        }
    }
}
