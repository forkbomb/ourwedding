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

namespace OurWedding.Views.ToDoList
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
            prepareBackStack();
            using (var db = DbConnection.GetConnection)
            {
                db.CreateTable<ToDoItem>();
                List<ToDoItem> toDoItems = (from i in db.Table<ToDoItem>() select i).ToList();
                toDoListView.ItemsSource = toDoItems;
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Add));
        }

        private void doItemSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            ToDoItem item = toggleSwitch.DataContext as ToDoItem;
            item.Done = toggleSwitch.IsOn;
            using (var db = DbConnection.GetConnection)
            {
                db.Update(item);
            }
        }

        private void prepareBackStack()
        {
            Frame frame = this.Frame;
            int stackSize = frame.BackStackDepth;
            if (stackSize > 1)
            {
                PageStackEntry navigatedFrom = frame.BackStack[stackSize - 1];
                PageStackEntry lastThis = frame.BackStack[stackSize - 2];
                if (navigatedFrom.SourcePageType == typeof(Add))
                {
                    frame.BackStack.Remove(navigatedFrom);
                    frame.BackStack.Remove(lastThis);
                }

            }

        }
    }
}
