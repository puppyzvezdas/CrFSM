using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIClient.DataRepositoryServiceReference;
using UIClient.Util;
using Common.model;
using System.Collections.ObjectModel;
using Common;
using System.ServiceModel;
using Common.PubSubCommon;
using System.Configuration;

namespace UIClient.ViewModel
{
    public class ViewModelCrew : ViewModelBase, IPublish
    {
        public RelayCommand AddEntityCommand { get; set; }
        public RelayCommand RemoveEntityCommand { get; set; }
        public RelayCommand AddToCrewCommand { get; set; }
        public RelayCommand RemoveFromCrewCommand { get; set; }
        public RelayCommand AddAllCommand { get; set; }
        public RelayCommand RemoveAllCommand { get; set; }

        public IdentifiedObject SelectedItem { get; set; }

        private ISubscribe proxy;

        private Crew crew;

        public string CrewName
        {
            get { return crew.CrewName; }
            set
            {
                crew.CrewName = value;
                RaisePropertyChanged("CrewName");
            }
        }


        public ObservableCollection<Crew> Crews { get; set; }
        public ObservableCollection<CrewMember> CrewMembers { get; set; }
        public ObservableCollection<CrewMember> AllMembers { get; set; }
        public ObservableCollection<CrewMember> SelectedMembers { get; set; }
        public ObservableCollection<CrewMember> SelectedMembersToRemove { get; set; }

        public ViewModelCrew()
        {
            if (Client.State == CommunicationState.Closed)
            {
                Client.Open();
            }

            crew = new Crew();

            AddEntityCommand = new RelayCommand(AddEntityCommand_Execute, AddEntityCommand_CanExecute);
            RemoveEntityCommand = new RelayCommand(RemoveEntityCommand_Execute, RemoveEntityCommand_CanExecute);
            AddToCrewCommand = new RelayCommand(AddToCrewCommand_Execute, AddToCrewCommand_CanExecute);
            RemoveFromCrewCommand = new RelayCommand(RemoveFromCrewCommand_Execute, RemoveFromCrewCommand_CanExecute);
            AddAllCommand = new RelayCommand(AddAllCommand_Execute, AddAllCommand_CanExecute);
            RemoveAllCommand = new RelayCommand(RemoveAllCommand_Execute, RemoveAllCommand_CanExecute);

            Crews = new ObservableCollection<Crew>(Client.GetEntityByType(EntityType.Crew).Cast<Crew>());
            AllMembers = new ObservableCollection<CrewMember>(Client.GetEntityByType(EntityType.CrewMember).Cast<CrewMember>().Where(member => string.IsNullOrEmpty(member.Crew)));
            CrewMembers = new ObservableCollection<CrewMember>();
            SelectedMembers = new ObservableCollection<CrewMember>();
            SelectedMembersToRemove = new ObservableCollection<CrewMember>();

            MakeProxy(this);

            try
            {
                proxy.Subscribe(EntityType.Crew.ToString());
                proxy.Subscribe(EntityType.CrewMember.ToString());
            }
            catch (Exception ex)
            {
                CrFSMLogger.CrFSMLogger.Instance.WriteToLog(ex);
            }
        }

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

        private bool AddEntityCommand_CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(CrewName);
        }

        private void AddEntityCommand_Execute(object parameter)
        {
            crew.Members.AddRange((from item in CrewMembers
                                   select item.GlobalId).ToList());
            Client.AddEntity(crew);
            Crews.Add(crew);
            crew = new Crew();
            CrewName = string.Empty;
            CrewMembers.Clear();
        }

        private bool RemoveEntityCommand_CanExecute(object parameter)
        {
            return SelectedItem != null;
        }

        private void RemoveEntityCommand_Execute(object parameter)
        {
            Client.RemoveEntity(SelectedItem);
            Crews.Remove(SelectedItem as Crew);
        }


        private bool AddToCrewCommand_CanExecute(object parameter)
        {
            return SelectedMembers.Count != 0;
        }

        private void AddToCrewCommand_Execute(object parameter)
        {
            foreach (var item in SelectedMembers)
            {
                CrewMembers.Add(item);
            }

            SelectedMembers.Clear();

            foreach (var item in CrewMembers)
            {
                AllMembers.Remove(item);
            }
        }

        private bool RemoveFromCrewCommand_CanExecute(object parameter)
        {
            return SelectedMembersToRemove.Count != 0;
        }

        private void RemoveFromCrewCommand_Execute(object parameter)
        {
            foreach (var item in SelectedMembersToRemove)
            {
                AllMembers.Add(item);
            }

            SelectedMembersToRemove.Clear();

            foreach (var item in AllMembers)
            {
                CrewMembers.Remove(item);
            }
        }

        private bool AddAllCommand_CanExecute(object parameter)
        {
            return AllMembers.Count != 0;
        }

        private void AddAllCommand_Execute(object parameter)
        {
            foreach (var item in AllMembers)
                CrewMembers.Add(item);
            AllMembers.Clear();
        }

        private bool RemoveAllCommand_CanExecute(object parameter)
        {
            return CrewMembers.Count != 0;
        }

        private void RemoveAllCommand_Execute(object parameter)
        {
            foreach (var item in CrewMembers)
                AllMembers.Add(item);
            CrewMembers.Clear();
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
            }
        }
        #endregion
    }
}
