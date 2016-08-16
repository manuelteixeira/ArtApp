using ArtApp.ViewModels;
using Xamarin.Forms;

namespace ArtApp.Views
{
    public partial class PathologiesView : ContentPage
    {
        public PathologiesView()
        {
            InitializeComponent();
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ((PathologiesViewModel)this.BindingContext).SearchPathologyCommand.Execute();
        }
    }
}
