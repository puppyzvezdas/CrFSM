using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIClient.Util;
using UIClient.DataRepositoryServiceReference;
using Common.model;
using System.Windows;
using System.ServiceModel;
using System.Windows.Controls;

namespace UIClient.ViewModel
{
    public class ViewModelLogInDialog : ViewModelBase, IDisposable
    {
        public RelayCommand LogInCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }

        private string username;
        public string Username
        {
            get { return username; }
            set 
            {
                username = value;
                RaisePropertyChanged("Username");
            }
        }

        DataRepositoryServiceClient client = new DataRepositoryServiceClient();

        public ViewModelLogInDialog()
        {
            LogInCommand = new RelayCommand(LogInCommand_Execute);
            CloseCommand = new RelayCommand(CloseCommand_Execute);
            client.Open();
        }

        private void LogInCommand_Execute(object parameter)
        {
            User user = null;
            try
            {
                user = client.GetUserByUsername(Username);
            }
            catch (FaultException ex)
            {
                CrFSMLogger.CrFSMLogger.Instance.WriteToLog(ex);
                MessageBox.Show(ex.Message, UIClient.Properties.Resources.MESSAGE_LOGIN_FAILED);
                return;
            }

            if (user.Password.CompareTo((parameter as PasswordBox).Password) == 0)
            {
                MainWindow mw = new MainWindow();
                mw.Show();
                var w = Application.Current.Windows[0];
                w.Close();
            }
            else
            {
                MessageBox.Show(string.Format("Wrong password for username {0}.", user.Username), UIClient.Properties.Resources.MESSAGE_LOGIN_FAILED);
            }
        }

        private void CloseCommand_Execute(object parameter)
        {
            Environment.Exit(0);
        }

        public void Dispose()
        {
            try
            {
                client.Close();
            }
            catch
            {
                client.Abort();
            }
        }
    }
}
