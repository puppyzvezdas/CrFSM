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
    /// Interaction logic for ViewUpdateCrewMember.xaml
    /// </summary>
    public partial class ViewUpdateCrewMember : Grid
    {
        public ViewUpdateCrewMember()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.AddedItems)
                (this.DataContext as ViewModelUpdateCrewMember).SelectedAllSkills.Add((Skills)item);
            foreach (var item in e.RemovedItems)
                (this.DataContext as ViewModelUpdateCrewMember).SelectedAllSkills.Remove((Skills)item);
        }

        private void ListView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.AddedItems)
                (this.DataContext as ViewModelUpdateCrewMember).SelectedMemberSkills.Add((Skills)item);
            foreach (var item in e.RemovedItems)
                (this.DataContext as ViewModelUpdateCrewMember).SelectedMemberSkills.Remove((Skills)item);
        }
    }
}
