﻿using Prism.Mvvm;
using ArtApp.Model;

namespace ArtApp.ViewModels
{
    public class WorkViewModel : BindableBase
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public WorkViewModel()
        {

        }
    }
}