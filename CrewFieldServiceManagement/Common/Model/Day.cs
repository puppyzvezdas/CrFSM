using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.model;
using System.Runtime.Serialization;

namespace Common.Model
{
    [DataContract]
    [KnownType(typeof(WorkingDay))]
    [KnownType(typeof(NonWorkingDay))]
    public abstract class Day : IdentifiedObject
    {
        private DateTime day;

        [DataMember]
        public DateTime ConcreteDay
        {
            get { return day; }
            set { day = value; }
        }

        public Day(EntityType _type) : base(_type) { }

        public override string ToString()
        {
            return this.day.ToString();
        } 
    }
}
