using ArtApp.Model.Interfaces;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace ArtApp.Model
{
    public class WorkAuthor : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(Work))]
        public int WorkId { get; set; }

        [ForeignKey(typeof(Author))]
        public int AuthorId { get; set; }

    }
}
