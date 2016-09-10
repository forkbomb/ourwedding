using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurWedding.Models
{
    class Guest
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public bool Confirmed { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
