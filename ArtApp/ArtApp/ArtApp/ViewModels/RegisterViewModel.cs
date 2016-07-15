using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace ArtApp.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private string _nome;
        private string _email;
        private string _password;

        public string Nome
        {
            get { return _nome; }
            set
            {
                _nome = value;
                this.OnPropertyChanged();
            }
        }

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


        public ICommand RegisterCommand { get; set; }

        public RegisterViewModel()
        {
            this.RegisterCommand = new Command(Register);

        }

        private void Register()
        {
            //Registar User na API
            
        }
    }
}
