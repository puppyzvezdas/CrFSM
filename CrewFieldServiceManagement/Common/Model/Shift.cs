using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.model;
using System.Runtime.Serialization;

namespace Common.Model
{
    [DataContract]
    public class Shift : IdentifiedObject
    {
        private string startTime;
        private string endTime;

        [DataMember]
        public string StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        [DataMember]
        public string EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        public Shift() : base(EntityType.Shift) { }

        public override string ToString()
        {
            return string.Format("{0} ({1}-{2})",this.Name, this.StartTime, this.EndTime);
        }
    }
}
