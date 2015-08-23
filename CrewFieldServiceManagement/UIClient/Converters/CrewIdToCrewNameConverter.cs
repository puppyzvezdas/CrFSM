using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Common.model;
using Common;

namespace UIClient.Converters
{
    public class CrewIdToCrewNameConverter : IValueConverter
    {

        DataRepositoryServiceReference.DataRepositoryServiceClient client = new DataRepositoryServiceReference.DataRepositoryServiceClient();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (client.State == System.ServiceModel.CommunicationState.Closed)
                client.Open();
            try
            {
                IdentifiedObject idObj = client.GetEntityById(EntityType.Crew, (string)value);
                return (idObj as Crew).CrewName;
            }
            catch
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
