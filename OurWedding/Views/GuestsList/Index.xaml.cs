using OurWedding.Models;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace OurWedding.Views.GuestsList
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Index : Page
    {
        ObservableCollection<Guest> guests = new ObservableCollection<Guest>();

        public Index()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            PrepareBackStack();
            using (var db = DbConnection.GetConnection)
            {
                db.CreateTable<Guest>();
                guests = new ObservableCollection<Guest>(from g in db.Table<Guest>().OrderByDescending(g => g.CreatedAt) select g);
                guestsListView.ItemsSource = guests;
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Add));
        }

        private void confirmGuestSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            Guest guest = toggleSwitch.DataContext as Guest;
            guest.Confirmed = toggleSwitch.IsOn;
            using (var db = DbConnection.GetConnection)
            {
                db.Update(guest);
            }
        }

        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            StackPanel stackPanel = sender as StackPanel;
            Guest guest = stackPanel.DataContext as Guest;
            int id = guest.Id;
            this.Frame.Navigate(typeof(Edit), id);
        }

        private void StackPanel_Holding(object sender, HoldingRoutedEventArgs e)
        {
            StackPanel panel = sender as StackPanel;
            FlyoutBase flyoutBase = FlyoutBase.GetAttachedFlyout(panel);
            flyoutBase.ShowAt(panel);
        }

        private void deleteItem_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = (sender as MenuFlyoutItem).DataContext as Guest;
            bool removed = guests.Remove(guest);
            if (removed)
            {
                using (var db = DbConnection.GetConnection)
                {
                    db.Delete(guest);
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
                if (navigatedFrom.SourcePageType == typeof(Add) || navigatedFrom.SourcePageType == typeof(Edit))
                {
                    frame.BackStack.Remove(navigatedFrom);
                    frame.BackStack.Remove(lastThis);
                }
            }
        }
    }
}
