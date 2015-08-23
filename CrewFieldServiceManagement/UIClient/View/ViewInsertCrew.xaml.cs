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
using Common.model;

namespace UIClient.View
{
    /// <summary>
    /// Interaction logic for ViewInserCrew.xaml
    /// </summary>
    public partial class ViewInsertCrew : Grid
    {
        public ViewInsertCrew()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.AddedItems)
                (this.DataContext as ViewModelCrew).SelectedMembers.Add(item as CrewMember);

            foreach (var item in e.RemovedItems)
                (this.DataContext as ViewModelCrew).SelectedMembers.Remove(item as CrewMember);
        }

        private void ListView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.AddedItems)
                (this.DataContext as ViewModelCrew).SelectedMembersToRemove.Add(item as CrewMember);

            foreach (var item in e.RemovedItems)
                (this.DataContext as ViewModelCrew).SelectedMembersToRemove.Remove(item as CrewMember);
        }
    }
}
