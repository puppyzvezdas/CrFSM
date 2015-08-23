using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UIClient.ViewModel;

namespace UIClient.View
{
    /// <summary>
    /// Interaction logic for ViewCrewMemberControl.xaml
    /// </summary>
    public partial class ViewCrewMemberControl : UserControl
    {
        public ViewCrewMemberControl()
        {
            InitializeComponent();
           // DataContext = new ViewModel.ViewModelCrewMember();
        }

        private void TabItem_Selected(object sender, RoutedEventArgs e)
        {
          //  (((sender as TabItem).Content as ViewUpdateCrewMember).DataContext as ViewModelUpdateCrewMember).CrewMemberId = (this.DataContext as ViewModelCrewMember).SelectedItem.GlobalId;
        }
    }
}
