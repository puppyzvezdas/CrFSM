using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.model;
using System.Runtime.Serialization;

namespace Common.Model
{
    [DataContract]
    public class Vacation : IdentifiedObject
    {
        private DateTime startDate;
        private TimeSpan duration;

        [DataMember]
        public TimeSpan Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        [DataMember]
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public Vacation() : base(EntityType.Vacation) { }
    }
}
