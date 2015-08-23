using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.model;
using System.Collections.ObjectModel;
using Common;
using UIClient.Util;
using System.ServiceModel;
using System.Collections;

namespace UIClient.ViewModel
{
    public class ViewModelUpdateCrewMember : ViewModelBase
    {
        public RelayCommand AddSkillCommand { get; set; }
        public RelayCommand RemoveSkillCommand { get; set; }
        public RelayCommand AddAllSkillsCommand { get; set; }
        public RelayCommand RemoveAllSkillsCommand { get; set; }
        public RelayCommand UpdateEntityCommand { get; set; }

        private CrewMember crewMember { get; set; }

        public ObservableCollection<Crew> Crews { get; set; }
        public ObservableCollection<CrewMember> CrewMembers { get; set; }
        public ObservableCollection<UserType> UserTypes { get; set; }

        public ObservableCollection<Skills> AllSkills { get; set; }
        public ObservableCollection<Skills> MemeberSkills { get; set; }

        public ObservableCollection<Skills> SelectedAllSkills { get; set; }
        public ObservableCollection<Skills> SelectedMemberSkills { get; set; }

        private string gid;
        public string CrewMemberId
        {
            get { return gid; }
            set
            {
                gid = value;
                RefreshLists();
            }
        }


        public string FirstName
        {
            get { return crewMember.FirstName; }
            set
            {
                crewMember.FirstName = value;
                RaisePropertyChanged("FirstName");
            }
        }

        public string FamilyName
        {
            get { return crewMember.FamilyName; }
            set
            {
                crewMember.FamilyName = value;
                RaisePropertyChanged("FamilyName");
            }
        }

        public string Email
        {
            get { return crewMember.Email; }
            set
            {
                crewMember.Email = value;
                RaisePropertyChanged("Email");
            }
        }

        public string Phone
        {
            get { return crewMember.Phone; }
            set
            {
                crewMember.Phone = value;
                RaisePropertyChanged("Phone");
            }
        }

        public string Username
        {
            get { return crewMember.Username; }
            set
            {
                crewMember.Username = value;
                RaisePropertyChanged("Username");
            }
        }

        public string Password
        {
            get { return crewMember.Password; }
            set
            {
                crewMember.Password = value;
                RaisePropertyChanged("Password");
            }
        }

        public Crew Crew
        {
            get
            {
                try
                {
                    return Client.GetEntityById(EntityType.Crew, crewMember.Crew) as Crew;
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                crewMember.Crew = (value as Crew).GlobalId;
                RaisePropertyChanged("Crew");
            }
        }

        public UserType UserType
        {
            get { return crewMember.UserType; }
            set
            {
                crewMember.UserType = value;
                RaisePropertyChanged("UserType");
            }
        }

        public ViewModelUpdateCrewMember()
        {
            if (Client.State == CommunicationState.Closed)
            {
                Client.Open();
            }

            crewMember = new CrewMember();

            AddSkillCommand = new RelayCommand(AddSkillCommand_Execute, AddSkillCommand_CanExecute);
            RemoveSkillCommand = new RelayCommand(RemoveSkillCommand_Execute, RemoveSkillCommand_CanExecute);
            AddAllSkillsCommand = new RelayCommand(AddAllSkillsCommand_Execute, AddAllSkillsCommand_CanExecute);
            RemoveAllSkillsCommand = new RelayCommand(RemoveAllSkillsCommand_Execute, RemoveAllSkillsCommand_CanExecute);
            UpdateEntityCommand = new RelayCommand(UpdateEntityCommand_Execute);

            Crews = new ObservableCollection<Crew>(Client.GetEntityByType(EntityType.Crew).Cast<Crew>());
            CrewMembers = new ObservableCollection<CrewMember>(Client.GetEntityByType(EntityType.CrewMember).Cast<CrewMember>());

            UserTypes = new ObservableCollection<UserType> { UserType.Manager, UserType.Worker };

            AllSkills = new ObservableCollection<Skills>
            {
                Skills.OperateInSubstation,
                Skills.OperateOutOfSubstation,
                Skills.PerformPlannedWork,
                Skills.PerformSwitching,
                Skills.PerformUnplannedWork
            };

            MemeberSkills = new ObservableCollection<Skills>();
            SelectedAllSkills = new ObservableCollection<Skills>();
            SelectedMemberSkills = new ObservableCollection<Skills>();

        }

        private bool AddSkillCommand_CanExecute(object parameter)
        {
            return SelectedAllSkills.Count != 0;
        }

        private void AddSkillCommand_Execute(object parameter)
        {
            foreach (var item in SelectedAllSkills)
                MemeberSkills.Add(item);

            SelectedAllSkills.Clear();

            foreach (var item in MemeberSkills)
                AllSkills.Remove(item);
        }

        private bool RemoveSkillCommand_CanExecute(object parameter)
        {
            return SelectedMemberSkills.Count != 0;
        }

        private void RemoveSkillCommand_Execute(object parameter)
        {
            foreach (var item in SelectedMemberSkills)
                AllSkills.Add(item);

            SelectedMemberSkills.Clear();

            foreach (var item in AllSkills)
                MemeberSkills.Remove(item);
        }

        private bool AddAllSkillsCommand_CanExecute(object parameter)
        {
            return AllSkills.Count != 0;
        }

        private void AddAllSkillsCommand_Execute(object parameter)
        {
            foreach (var item in AllSkills)
                MemeberSkills.Add(item);

            AllSkills.Clear();
        }

        private bool RemoveAllSkillsCommand_CanExecute(object parameter)
        {
            return MemeberSkills.Count != 0;
        }

        private void RemoveAllSkillsCommand_Execute(object parameter)
        {
            foreach (var item in MemeberSkills)
                AllSkills.Add(item);

            MemeberSkills.Clear();
        }

        private void UpdateEntityCommand_Execute(object parameter)
        {
            foreach (var item in MemeberSkills)
                if (!crewMember.Skills.Contains(item))
                    crewMember.Skills.Add(item);
            Client.UpdateEntity(crewMember);
        }

        private void RefreshLists()
        {
            MemeberSkills.Clear();
            crewMember = Client.GetEntityById(EntityType.CrewMember, gid) as CrewMember;
            foreach (var item in crewMember.Skills)
            {
                MemeberSkills.Add(item);
                AllSkills.Remove(item);
            }
            RaisePropertyChanged("FirstName");
            RaisePropertyChanged("FamilyName");
            RaisePropertyChanged("Email");
            RaisePropertyChanged("Phone");
            RaisePropertyChanged("Username");
            RaisePropertyChanged("Password");
            RaisePropertyChanged("UserType");
            try
            {
                Crew = Client.GetEntityById(EntityType.Crew, crewMember.Crew) as Crew;
            }
            catch { }
        }
    }
}
