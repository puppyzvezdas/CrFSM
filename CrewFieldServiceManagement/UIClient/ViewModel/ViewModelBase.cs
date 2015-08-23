using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using UIClient.DataRepositoryServiceReference;

namespace UIClient.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {

        internal void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public DataRepositoryServiceClient Client { get; set; }

        public ViewModelBase()
        {

            Client = new DataRepositoryServiceClient();
        }
    }
}
