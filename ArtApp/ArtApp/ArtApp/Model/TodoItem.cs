using SQLite.Net.Attributes;

namespace ArtApp.Model
{
    public class TodoItem
    {


        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
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
