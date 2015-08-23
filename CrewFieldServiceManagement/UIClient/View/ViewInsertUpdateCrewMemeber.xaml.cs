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
using Common;

namespace UIClient.View
{
    /// <summary>
    /// Interaction logic for ViewInsertUpdateCrewMemeber.xaml
    /// </summary>
    public partial class ViewInsertUpdateCrewMemeber : Grid
    {
        public ViewInsertUpdateCrewMemeber()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.AddedItems)
                (this.DataContext as ViewModelCrewMember).SelectedAllSkills.Add((Skills)item);
            foreach (var item in e.RemovedItems)
                (this.DataContext as ViewModelCrewMember).SelectedAllSkills.Remove((Skills)item);
        }

        private void ListView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.AddedItems)
                (this.DataContext as ViewModelCrewMember).SelectedMemberSkills.Add((Skills)item);
            foreach (var item in e.RemovedItems)
                (this.DataContext as ViewModelCrewMember).SelectedMemberSkills.Remove((Skills)item);
        }
    }
}
