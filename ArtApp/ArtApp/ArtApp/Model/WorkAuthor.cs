using SQLiteNetExtensions.Attributes;

namespace ArtApp.Model
{
    public class WorkAuthor
    {
        [ForeignKey(typeof(Work))]
        public int WorkId { get; set; }

        [ForeignKey(typeof(Author))]
        public int AuthorId { get; set; }

    }
}
