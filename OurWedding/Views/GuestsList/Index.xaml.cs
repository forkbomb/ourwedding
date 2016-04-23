using OurWedding.Models;
using System;
using System.Collections.Generic;
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
        public Index()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            using (var db = DbConnection.GetConnection)
            {
                db.CreateTable<Guest>();
                List<Guest> guests = (from g in db.Table<Guest>() select g).ToList();
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
    }
}
