using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
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

    class ToDoItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Task { get; set; }
        public Priority Priority { get; set; } = Priority.Normal;
        public bool Done { get; set; } = false;        
    }
}
