using ArtApp.ViewModels;
using Xamarin.Forms;

namespace ArtApp.Views
{
    public partial class ConditionReportsView : ContentPage
    {
        public ConditionReportsView()
        {
            InitializeComponent();
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ((ConditionReportsViewModel)this.BindingContext).SearchConditionReportCommand.Execute();
        }
    }
}
