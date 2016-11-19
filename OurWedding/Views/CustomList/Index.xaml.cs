using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using OurWedding.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace OurWedding.Views.CustomList
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Index : Page
    {
        ObservableCollection<Models.CustomList> customLists = new ObservableCollection<Models.CustomList>();

        public Index()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            PrepareBackStack();
            using (var db = DbConnection.GetConnection)
            {
                customLists = new ObservableCollection<Models.CustomList>(db.Table<Models.CustomList>().ToList());
            }
            customListsListView.ItemsSource = customLists;
        }

        private void StackPanel_Holding(object sender, HoldingRoutedEventArgs e)
        {
            StackPanel panel = sender as StackPanel;
            FlyoutBase flyoutBase = FlyoutBase.GetAttachedFlyout(panel);
            flyoutBase.ShowAt(panel);
        }

        private void deleteItem_Click(object sender, RoutedEventArgs e)
        {
            Models.CustomList customList = (sender as MenuFlyoutItem).DataContext as Models.CustomList;
            bool removed = customLists.Remove(customList);
            if (removed)
            {
                using (var db = DbConnection.GetConnection)
                {
                    db.Delete(customList);
                }
            }
        }

        private void PrepareBackStack()
        {
            Frame frame = this.Frame;
            int stackSize = frame.BackStackDepth;
            if (stackSize > 1)
            {
                PageStackEntry navigatedFrom = frame.BackStack[stackSize - 1];
                PageStackEntry lastThis = frame.BackStack[stackSize - 2];
                //if (navigatedFrom.SourcePageType == typeof(Add))
                //{
                //    frame.BackStack.Remove(navigatedFrom);
                //    frame.BackStack.Remove(lastThis);
                //}
            }
        }
    }
}
