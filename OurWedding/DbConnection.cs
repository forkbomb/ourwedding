using OurWedding.Models;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace OurWedding
{
    public class DbConnection
    {
        public static SQLiteConnection GetConnection
        {
            get
            {
                return new SQLiteConnection(
                    new SQLitePlatformWinRT(),
                    Path.Combine(ApplicationData.Current.LocalFolder.Path, "OurWeddingStorage.sqlite"));
            }
        }

        public static void SeedDatabase()
        {
            using (var db = GetConnection)
            {
                db.CreateTable<Guest>();
                db.CreateTable<ToDoItem>();
                db.CreateTable<CustomListItem>();
                db.CreateTable<CustomList>();
                CustomList list;
                foreach (string listName in new string[] { "kwiaciarnie", "sukienki", "buty" })
                {
                    list = new CustomList();
                    list.Name = listName;
                    db.Insert(list);
                }
            }
        }
    }
}
