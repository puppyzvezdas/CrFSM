using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using UIClient.DataRepositoryServiceReference;
using Common;

namespace UIClient.Converters
{
    public class UserTypeToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((UserType)value == UserType.Manager)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
            {
                return UserType.Manager;
            }
            else
            {
                return UserType.Worker;
            }
        }
    }
}
