﻿using OurWedding.Models;
using OurWedding.Views;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace OurWedding
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            using (var db = DbConnection.GetConnection)
            {
                int confirmedGuestsCount = db.Table<Guest>().Where(x => x.Confirmed == true).Sum(x => x.Adults + x.Children);
                int allGuestsCount = db.Table<Guest>().Sum(x => x.Adults + x.Children);
                this.confirmedGuests.Text = confirmedGuestsCount.ToString();
                this.invitedGuests.Text = allGuestsCount.ToString();

                int toDoItemsCount = db.Table<ToDoItem>().Where(x => x.Done == false).Count();
                this.remainingToDoItems.Text = toDoItemsCount.ToString();
            }
        }
        
        private void GuestList_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Views.GuestsList.Index));
        }

        private void ToDoList_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Views.ToDoList.Index));
        }

        private void CustomLists_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Views.CustomList.Index));
        }
    }
}
