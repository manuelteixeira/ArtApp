using System;
using ArtApp.Models;
using ArtApp.ViewModels;
using Xamarin.Forms;

namespace ArtApp.Views
{
    public partial class WorkView : ContentPage
    {
        public WorkView()
        {
            InitializeComponent();
        }

        public WorkView(WorksViewModelOLD workViewModel)
        {
            InitializeComponent();

            BindingContext = workViewModel;


        }

       
    }
}
