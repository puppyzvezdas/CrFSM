using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Common;
using Common.model;
using System.Collections;
using Common.Model;

namespace DataRepositoryServiceLib
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IDataRepositoryService
    {
        [OperationContract]
        void AddEntity(IdentifiedObject _entity);

        [OperationContract]
        void RemoveEntity(IdentifiedObject _entity);

        [OperationContract]
        IdentifiedObject GetEntityById(EntityType _type, string id);

        [OperationContract]
        List<IdentifiedObject> GetEntityByType(EntityType _type);

        [OperationContract]
        User GetUserByUsername(string _username);

        [OperationContract]
        void UpdateEntity(IdentifiedObject _entity);

        [OperationContract]
        int CreateWorkingDays();

        [OperationContract]
        WorkingDay GetDayOfYear(DateTime _day);
    }
}
