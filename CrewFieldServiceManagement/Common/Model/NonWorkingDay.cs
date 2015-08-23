using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.model;
using System.Runtime.Serialization;

namespace Common.Model
{
    [DataContract]
    public class NonWorkingDay : Day
    {
     
        public NonWorkingDay() : base(EntityType.NonWorkingDay) { }

        public NonWorkingDay(DateTime _day) : base(EntityType.NonWorkingDay) 
        {
            this.ConcreteDay = _day;
        }

        public override string ToString()
        {
            return this.ConcreteDay.ToString();
        }
    }
}
