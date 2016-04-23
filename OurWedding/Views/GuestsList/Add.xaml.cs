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
    public sealed partial class Add : Page
    {
        public Add()
        {
            this.InitializeComponent();
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text;
            int adults = Int32.Parse(adultsTextBox.Text);
            int children = Int32.Parse(childrenTextBox.Text);
            string comment = commentTextBox.Text;
            Guest guest = new Guest();
            guest.Name = name;
            guest.Adults = adults;
            guest.Children = children;
            guest.Comment = comment;
            using (var db = DbConnection.GetConnection)
            {
                db.Insert(guest);
            }
            this.Frame.Navigate(typeof(Index));
        }
    }
}
