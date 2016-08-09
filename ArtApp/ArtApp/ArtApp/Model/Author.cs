using System.Collections.Generic;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace ArtApp.Model
{
    public class Author
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Name { get; set; }

        [ManyToMany(typeof(WorkAuthor), CascadeOperations = CascadeOperation.CascadeRead)]
        public List<Work> Works { get; set; }
    }
}
