using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtApp.Pages;
using ArtApp.ViewModels;
using Xamarin.Forms;

namespace ArtApp.Views
{
    public partial class WorksPage : ContentPage
    {
        public WorksViewModel ViewModel
        {
            get
            {
                return this.BindingContext as WorksViewModel;
            }
            set { this.BindingContext = value; }
        }

        public WorksPage()
        {
            this.ViewModel = new WorksViewModel();

            InitializeComponent();

            createWorkButton.Clicked += CreateWorkButton_Clicked;
        }

        private void CreateWorkButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewWorkPage());
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.ViewModel.SearchCommand.CanExecute(null))
            {
                this.ViewModel.SearchCommand.Execute(null);
            }
        }
    }
}
