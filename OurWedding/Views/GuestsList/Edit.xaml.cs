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
    public sealed partial class Edit : Page
    {
        Guest guest;
        public Edit()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is int)
            {
                using (var db = DbConnection.GetConnection)
                {
                    int guestId = (e.Parameter as int?).Value;
                    guest = (from g in db.Table<Guest>() where g.Id == guestId select g).FirstOrDefault();
                    FillFields();
                }
            }
            // TODO else flash error and go back
            base.OnNavigatedTo(e);
        }

        private void FillFields()
        {
            if (guest != null)
            {
                nameTextBox.Text = guest.Name;
                adultsTextBox.Text = guest.Adults.ToString();
                childrenTextBox.Text = guest.Children.ToString();
                commentTextBox.Text = guest.Comment ?? "";
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text;
            int adults = Int32.Parse(adultsTextBox.Text);
            int children = Int32.Parse(childrenTextBox.Text);
            string comment = commentTextBox.Text;
            guest.Name = name;
            guest.Adults = adults;
            guest.Children = children;
            guest.Comment = comment;
            using (var db = DbConnection.GetConnection)
            {
                db.Update(guest);
            }
            this.Frame.Navigate(typeof(Index));
        }

    }
}
