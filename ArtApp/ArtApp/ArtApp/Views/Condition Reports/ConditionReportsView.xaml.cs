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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((ConditionReportsViewModel)this.BindingContext).RefreshConditionReportsListCommand.Execute();
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ((ConditionReportsViewModel)this.BindingContext).SearchConditionReportCommand.Execute();
        }
    }
}
