using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIClient.DataRepositoryServiceReference;
using UIClient.Util;
using Common.model;
using System.ServiceModel;
using System.Collections.ObjectModel;
using Common;
using Common.Model;
using Common.PubSubCommon;
using System.Configuration;

namespace UIClient.ViewModel
{
    public class ViewModelCrewMember : ViewModelBase, IPublish
    {
        #region Commands
        public RelayCommand AddEntityCommand { get; set; }
        public RelayCommand RemoveEntityCommand { get; set; }
        public RelayCommand AddSkillCommand { get; set; }
        public RelayCommand RemoveSkillCommand { get; set; }
        public RelayCommand AddAllSkillsCommand { get; set; }
        public RelayCommand RemoveAllSkillsCommand { get; set; }
        #endregion

        #region Fields
        private CrewMember selectedMember;
        private CrewMember crewMember { get; set; }

        private ISubscribe proxy;
        #endregion

        #region Observable Collections
        public ObservableCollection<Crew> Crews { get; set; }
        public ObservableCollection<CrewMember> CrewMembers { get; set; }
        public ObservableCollection<UserType> UserTypes { get; set; }
        public ObservableCollection<Shift> AllShifts { get; set; }

        public ObservableCollection<Skills> AllSkills { get; set; }
        public ObservableCollection<Skills> MemeberSkills { get; set; }

        public ObservableCollection<Skills> SelectedAllSkills { get; set; }
        public ObservableCollection<Skills> SelectedMemberSkills { get; set; }
        #endregion

        #region Properties
        public CrewMember SelectedItem
        {
            get
            {
                return selectedMember;
            }
            set
            {
                selectedMember = value;
                RaisePropertyChanged("SelectedItem");
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

        public Shift SelectedShift
        {
            get
            {
                try
                {
                    return Client.GetEntityById(EntityType.Shift, crewMember.Shift) as Shift;
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                crewMember.Shift = (value as Shift).GlobalId;
                RaisePropertyChanged("SelectedShift");
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
        #endregion

        #region Constructors
        public ViewModelCrewMember()
        {
            if (Client.State == CommunicationState.Closed)
            {
                Client.Open();
            }

            crewMember = new CrewMember();

            AddEntityCommand = new RelayCommand(AddEntityCommand_Execute);
            RemoveEntityCommand = new RelayCommand(RemoveEntityCommand_Execute, RemoveEntityCommand_CanExecute);
            AddSkillCommand = new RelayCommand(AddSkillCommand_Execute, AddSkillCommand_CanExecute);
            RemoveSkillCommand = new RelayCommand(RemoveSkillCommand_Execute, RemoveSkillCommand_CanExecute);
            AddAllSkillsCommand = new RelayCommand(AddAllSkillsCommand_Execute, AddAllSkillsCommand_CanExecute);
            RemoveAllSkillsCommand = new RelayCommand(RemoveAllSkillsCommand_Execute, RemoveAllSkillsCommand_CanExecute);

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
            AllShifts = new ObservableCollection<Shift>(Client.GetEntityByType(EntityType.Shift).Cast<Shift>());

            MakeProxy(this);

            try
            {
                proxy.Subscribe(EntityType.CrewMember.ToString());
                proxy.Subscribe(EntityType.Crew.ToString());
                proxy.Subscribe(EntityType.Shift.ToString());
            }
            catch (Exception ex)
            {
                CrFSMLogger.CrFSMLogger.Instance.WriteToLog(ex);
            }
        }
        #endregion

        #region Command implementations
        private void AddEntityCommand_Execute(object parameter)
        {
            crewMember.Skills.AddRange(MemeberSkills.ToList());
            Client.AddEntity(crewMember);
            crewMember = new CrewMember();
        }

        private bool RemoveEntityCommand_CanExecute(object parameter)
        {
            return SelectedItem != null;
        }

        private void RemoveEntityCommand_Execute(object parameter)
        {
            Client.RemoveEntity(SelectedItem);
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
        #endregion

        private void MakeProxy(object callbackinstance)
        {
            try
            {
                NetTcpBinding netTcpbinding = new NetTcpBinding(SecurityMode.None);
                TimeSpan ts = new TimeSpan(1, 0, 0);
                netTcpbinding.CloseTimeout = ts;
                netTcpbinding.OpenTimeout = ts;
                netTcpbinding.SendTimeout = ts;
                netTcpbinding.ReceiveTimeout = ts;
                EndpointAddress endpointAddress = new EndpointAddress(ConfigurationManager.AppSettings["EndpointAddress"]);
                InstanceContext context = new InstanceContext(callbackinstance);

                DuplexChannelFactory<ISubscribe> channelFactory = new DuplexChannelFactory<ISubscribe>(new InstanceContext(this), netTcpbinding, endpointAddress);
                proxy = channelFactory.CreateChannel();

            }
            catch (Exception ex)
            {
                CrFSMLogger.CrFSMLogger.Instance.WriteToLog(ex);
            }
        }

        #region IPublish members
        public void Publish(Message e, string topicName)
        {
            if (e != null)
            {

                if (topicName.CompareTo(EntityType.CrewMember.ToString()) == 0)
                {
                    CrewMembers.Clear();
                    foreach (IdentifiedObject item in e.IdObjCollection)
                    {
                        CrewMembers.Add(item as CrewMember);
                    }
                    return;
                }

                if (topicName.CompareTo(EntityType.Crew.ToString()) == 0)
                {
                    Crews.Clear();
                    foreach (IdentifiedObject item in e.IdObjCollection)
                    {
                        Crews.Add(item as Crew);
                    }
                    return;
                }

                if (topicName.CompareTo(EntityType.Shift.ToString()) == 0)
                {
                    AllShifts.Clear();
                    foreach (IdentifiedObject item in e.IdObjCollection)
                    {
                        AllShifts.Add(item as Shift);
                    }
                    return;
                }
            }
        }
        #endregion
    }
}
