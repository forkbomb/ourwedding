using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurWedding.Models
{
    class CustomListItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; private set; }

        public CustomList Parent()
        {
            using (var db = DbConnection.GetConnection)
            {
                db.CreateTable<CustomList>();
                return db.Table<CustomList>().FirstOrDefault(x => x.Id == this.ParentId);
            }
        }
    }
}
