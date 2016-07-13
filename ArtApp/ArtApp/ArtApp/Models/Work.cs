using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtApp.Models
{

    public enum Classification
    {
        Extremelly_Fragile,
        Fragile,
        Regular,
        Good,
        Extremelly_Good,
    };

    public class Work : INotifyPropertyChanged
    {
        public int WorkId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Technique { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Heigth { get; set; }
        public Classification Classification { get; set; }
        public List<Author> Authors { get; set; }



        public event PropertyChangedEventHandler PropertyChanged;
    }

   
}
