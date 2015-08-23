using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Common.model;

namespace Common.PubSubCommon
{
    [DataContract]
    public class Message
    {
        private string _topicName;

        private string _eventData;

        private List<IdentifiedObject> idObjCollection;

        [DataMember]
        public List<IdentifiedObject> IdObjCollection
        {
            get
            {
                if (idObjCollection == null)
                    idObjCollection = new List<IdentifiedObject>();
                return idObjCollection;
            }
            set { idObjCollection = value; }
        }

        [DataMember]
        public string TopicName { get { return _topicName; } set { _topicName = value; } }

        [DataMember]
        public string EventData { get { return _eventData; } set { _eventData = value; } }
    }
}
