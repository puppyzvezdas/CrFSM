using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Common.model;
using Common;

namespace UIClient.ViewModel
{
    public class ViewModelAssignment : ViewModelBase
    {
        public ObservableCollection<Worker> AvaliableWorkers { get; set; }
        public ObservableCollection<Worker> AssignedWorkers { get; set; }

        public ObservableCollection<Skills> AllSkills { get; set; }
        public ObservableCollection<Skills> RequiredSkills { get; set; }

        

        public ViewModelAssignment()
        {

            AllSkills = new ObservableCollection<Skills>
            {
                Skills.OperateInSubstation,
                Skills.OperateOutOfSubstation,
                Skills.PerformPlannedWork,
                Skills.PerformSwitching,
                Skills.PerformUnplannedWork
            };
                
            AvaliableWorkers = new ObservableCollection<Worker>();
            var collection1 = Client.GetEntityByType(EntityType.CrewMember).Where(o => string.IsNullOrEmpty((o as CrewMember).Crew));
            var collection2 = Client.GetEntityByType(EntityType.Crew);

            foreach (var item in collection1)
                AvaliableWorkers.Add(item as Worker);
            foreach (var item in collection2)
                AvaliableWorkers.Add(item as Worker);
        }
    }
}
