using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace ArtApp.Model
{
    public class WorkProject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(Work))]
        public int WorkId { get; set; }

        [ForeignKey(typeof(Project))]
        public int ProjectId { get; set; }
    }
}
