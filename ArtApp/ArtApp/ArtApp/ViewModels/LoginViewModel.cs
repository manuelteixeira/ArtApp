using System.Windows.Input;
using Xamarin.Forms;

namespace ArtApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _email;
        private string _password;

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                this.OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                this.OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }


        public LoginViewModel()
        {
            this.LoginCommand = new Command(this.Login);
            this.RegisterCommand = new Command(this.Register);

            
        }


        #region Methods for commands
        private async void Login()
        {
            //Comunicação com API

            //DUMMY LOGIN
            if (this.Email == "a" && this.Password == "a")
            {
                await this._messageService.ShowASync("Welcome " + this.Email);
                //Navegar para o inicio
                await this._navigationService.NavigateToMain();
            }
            else
            {
                //msg de erro
                await this._messageService.ShowASync("Email and/or password invalid");
            }
        }

        private async void Register()
        {
            await this._navigationService.NavigateToRegister();

        }
        #endregion
    }
        
        
}
