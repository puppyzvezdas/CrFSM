using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Common.model
{
    [Serializable]
    [DataContract]
    public class Crew : Worker
    {
        private List<string> members;
        private string crewName;

        [DataMember]
        public string CrewName
        {
            get { return crewName; }
            set
            {
                crewName = value;
                Name = crewName;
            }
        }

        [DataMember]
        public List<string> Members
        {
            get { return members; }
            set { members = value; }
        }

        public Crew()
            : base(EntityType.Crew)
        {
            members = new List<string>(1);
        }

        public Crew(string _crewName)
            : base(EntityType.Crew)
        {
            this.crewName = _crewName;
            this.members = new List<string>(1);
        }

        public override string ToString()
        {
            return this.crewName;
        }
    }
}
