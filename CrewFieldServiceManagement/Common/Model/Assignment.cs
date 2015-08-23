using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Common.model
{
    [DataContract]
    public class Assignment : IdentifiedObject
    {
        private DateTime assignedTime;
        private TimeSpan duration;
        private List<string> Workers;

        [DataMember]
        public DateTime AssignedTime
        {
            get { return this.assignedTime; }
            set { this.assignedTime = value; }
        }

        [DataMember]
        public TimeSpan Duration
        {
            get { return this.duration; }
            set { this.duration = value; }
        }

        public Assignment()
            : base(EntityType.Assignment)
        {
        }

        public Assignment(DateTime _assignedTime, TimeSpan _duration)
            : base(EntityType.Assignment)
        {
            this.assignedTime = _assignedTime;
            this.duration = _duration;
        }
    }
}
