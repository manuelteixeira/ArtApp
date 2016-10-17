using ArtApp.ViewModels;
using Xamarin.Forms;

namespace ArtApp.Views
{
    public partial class TodoItemsView : ContentPage
    {
        public TodoItemsView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((TodoItemsViewModel)this.BindingContext).ResfreshTodoItemsListCommand.Execute();
        }
    }
}
