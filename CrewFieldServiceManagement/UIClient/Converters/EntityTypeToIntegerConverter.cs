using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Common;

namespace UIClient.Converters
{
    public class EntityTypeToIntegerConverter: IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            EntityType type = (EntityType)value;

            switch (type)
            {
                case EntityType.CrewMember: return 0;
                case EntityType.Crew: return 1;
                case EntityType.Assignment: return 2;
                default: return 0;
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int index = (int)value;
            switch (index)
            {
                case 0: return EntityType.CrewMember;
                case 1: return EntityType.Crew;
                case 2: return EntityType.Assignment;
                default: return EntityType.CrewMember;
            }
        }
    }
}
