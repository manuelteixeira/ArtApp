using ArtApp.Model.Interfaces;
using SQLite.Net.Attributes;

namespace ArtApp.Model
{
    public class TodoItem : IEntity
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public bool Done { get; set; }

        [Ignore]
        public string DoneText
        {
            get { return Done ? "Iten has been completed" : "Still to do"; }
        }
    }
}
