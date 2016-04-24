using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurWedding.Models
{
    enum Priority
    {
        Low,
        Normal,
        High,
        Urgent
    };

    class ToDoItem : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Task { get; set; }
        public Priority Priority { get; set; } = Priority.Normal;
        public bool Done { get; set; } = false;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
