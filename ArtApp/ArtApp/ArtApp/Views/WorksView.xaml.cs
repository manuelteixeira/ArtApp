using ArtApp.ViewModels;
using Xamarin.Forms;

namespace ArtApp.Views
{
    public partial class WorksView : ContentPage
    {
        public WorksView()
        {
            InitializeComponent();
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ((WorksViewModel)this.BindingContext).SearchWorkCommand.Execute();
        }


    }
}
