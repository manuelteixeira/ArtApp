using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace ArtApp.Model
{
    public class WorkAuthor
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [ForeignKey(typeof(Work))]
        public int WorkId { get; set; }

        [ForeignKey(typeof(Author))]
        public int AuthorId { get; set; }

    }
}
