using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurWedding.Models
{
    public class CustomList
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<CustomListItem> Items()
        {
            using (var db = DbConnection.GetConnection)
            {
                return db.Table<CustomListItem>().Where(x => x.ParentId == this.Id).ToList<CustomListItem>();
            }
        }
    }
}
