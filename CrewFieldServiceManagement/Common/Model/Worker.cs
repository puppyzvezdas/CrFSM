using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Common.model
{
    [Serializable]
    [DataContract(Name = "Worker")]
    [KnownType(typeof(CrewMember))]
    [KnownType(typeof(Crew))]
    public abstract class Worker : IdentifiedObject
    {
        #region Fields
        private List<string> assignments;
        private AvaliabilityState avaliability = AvaliabilityState.Avaliable;

        #endregion

        #region Properties
        [DataMember]
        [XmlElement("Assignments")]
        public List<string> Assignments
        {
            get { return this.assignments; }
            set { this.assignments = value; }
        }
        [DataMember]
        public AvaliabilityState Avaliability
        {
            get { return avaliability; }
            set { avaliability = value; }
        }
        #endregion

        #region Constructors
        public Worker(EntityType _type)
            : base(_type)
        {
            this.assignments = new List<string>(1);
        }
        #endregion
    }
}
