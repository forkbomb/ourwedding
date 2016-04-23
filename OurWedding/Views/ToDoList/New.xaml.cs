﻿using OurWedding.Models;
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

namespace OurWedding.Views.ToDoList
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class New : Page
    {
        public New()
        {
            this.InitializeComponent();
        }
        
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text;
            ToDoItem item = new ToDoItem();
            item.Task = name;
            using (var db = DbConnection.GetConnection)
            {
                db.Insert(item);
            }
            this.Frame.Navigate(typeof(List));
        }
    }
}
