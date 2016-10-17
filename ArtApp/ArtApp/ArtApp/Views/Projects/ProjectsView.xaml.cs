using ArtApp.ViewModels;
using Xamarin.Forms;

namespace ArtApp.Views
{
    public partial class ProjectsView : ContentPage
    {
        public ProjectsView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((ProjectsViewModel)this.BindingContext).RefreshProjectListCommand.Execute();
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ((ProjectsViewModel)this.BindingContext).SearchProjectCommand.Execute();

        }
    }
}
