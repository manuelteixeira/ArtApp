using System.Collections.Generic;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace ArtApp.Model
{
    public enum Classification
    {
        Extremelly_Fragile,
        Fragile,
        Regular,
        Good,
        Extremelly_Good,
    };

    public class Work
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        //public DateTime Date { get; set; }
        //public string Technique { get; set; }
        //public float Length { get; set; }
        //public float Width { get; set; }
        //public float Heigth { get; set; }
        //public Classification Classification { get; set; }

        [ManyToMany(typeof(WorkAuthor))]
        public ICollection<Author> Authors { get; set; }



    }
}
