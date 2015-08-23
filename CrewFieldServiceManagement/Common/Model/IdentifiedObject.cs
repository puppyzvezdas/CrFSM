using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.Model;

namespace Common.model
{
    [Serializable]
    [DataContract]
    [KnownType(typeof(Worker))]
    [KnownType(typeof(Assignment))]
    [KnownType(typeof(User))]
    [KnownType(typeof(Shift))]
    [KnownType(typeof(Day))]
    public abstract class IdentifiedObject
    {
        private string globalId;
        private string name;
        private EntityType type;

        [DataMember]
        [XmlAttribute("GlobalId")]
        public string GlobalId
        {
            get { return globalId; }
            set { globalId = value; }
        }

        [DataMember]
        [XmlAttribute("Name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        [XmlAttribute("Type")]
        public EntityType Type
        {
            get { return type; }
            set { type = value; }
        }

        public IdentifiedObject()
        {
            this.globalId = Guid.NewGuid().ToString();
        }

        public IdentifiedObject(string _name)
        {
            this.globalId = Guid.NewGuid().ToString();
            this.name = _name;
        }

        public IdentifiedObject(EntityType _type)
        {
            this.globalId = Guid.NewGuid().ToString();
            this.type = _type;
        }
    }
}
