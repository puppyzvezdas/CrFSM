using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIClient.Util;
using Common.Model;
using System.Collections.ObjectModel;
using Common;

namespace UIClient.ViewModel
{
    public class ViewModelInsertShift : ViewModelBase
    {
        public RelayCommand AddShiftCommand { get; set; }

        public ObservableCollection<Shift> AllShifts { get; set; }

        private Shift shift = new Shift();

        public string ShiftName
        {
            get
            {
                return shift.Name;
            }

            set
            {
                shift.Name = value;
                RaisePropertyChanged("ShiftName");
            }
        }

        public DateTime StartTime
        {
            get
            {
                string[] array = shift.StartTime != null ? shift.StartTime.Split(':') : new string[2] { "0", "0" };
                return new DateTime(1, 1, 1, Convert.ToInt32(array[0]), Convert.ToInt32(array[1]), 0);
            }
            set
            {
                this.shift.StartTime = string.Format("{0:00}:{1:00}", value.Hour, value.Minute);
                RaisePropertyChanged("StartTime");
            }
        }

        public DateTime EndTime
        {
            get
            {
                string[] array = shift.EndTime != null ? shift.EndTime.Split(':') : new string[2] { "0", "0" };
                return new DateTime(1, 1, 1, Convert.ToInt32(array[0]), Convert.ToInt32(array[1]), 0);
            }
            set
            {
                this.shift.EndTime = string.Format("{0:00}:{1:00}", value.Hour, value.Minute);
                RaisePropertyChanged("EndTime");
            }
        }

        public ViewModelInsertShift()
        {
            AddShiftCommand = new RelayCommand(AddShiftCommand_Execute);

            AllShifts = new ObservableCollection<Shift>(Client.GetEntityByType(EntityType.Shift).Cast<Shift>().ToList());
        }

        private void AddShiftCommand_Execute(object parameter)
        {
            Client.AddEntity(shift);
            AllShifts.Add(shift);
            shift = new Shift();
        }
    }
}
