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

        [ForeignKey(typeof(Work))]
        public int WorkId { get; set; }

        [ManyToMany(typeof(WorkAuthor))]
        public ICollection<Work> Works { get; set; }
    }
}
