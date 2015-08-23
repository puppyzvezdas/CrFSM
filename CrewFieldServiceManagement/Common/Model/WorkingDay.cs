using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.model;
using System.Runtime.Serialization;

namespace Common.Model
{
    [DataContract]
    public class WorkingDay : Day
    {

        public WorkingDay() : base(EntityType.WorkingDay) { }

        public WorkingDay(DateTime _day) : base(EntityType.WorkingDay) 
        {
            this.ConcreteDay = _day;
        }

        public override string ToString()
        {
            return this.ConcreteDay.ToString();
        } 
    }
}
