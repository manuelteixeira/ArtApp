using ArtApp.ViewModels;
using Xamarin.Forms;

namespace ArtApp.Views
{
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void PasswordEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ((LoginViewModel) this.BindingContext).CanLogin = true;
        }
    }
}
