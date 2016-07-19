using System;
using System.Collections.Generic;
using System.Windows.Input;
using ArtApp.Models;
using ArtApp.Services;
using Xamarin.Forms;

namespace ArtApp.ViewModels
{
    public class WorkViewModel : BaseViewModel
    {

        #region Properties
        public string WorkId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Technique { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Heigth { get; set; }
        public Classification Classification { get; set; }
        public List<Author> Authors { get; set; }

        #endregion

        public WorkViewModel()
        {
        }


    }
}
