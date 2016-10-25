using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using Prism.Services;

namespace ArtApp.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;

        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public DelegateCommand LoginCommand { get; private set; }
        public DelegateCommand RegisterCommand { get; private set; }
        public DelegateCommand CanLoginCommand { get; private set; } 

        public LoginViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this._navigationService = navigationService;
            this._pageDialogService = pageDialogService;

            this.LoginCommand = new DelegateCommand(this.Login, this.CanLogin).ObservesProperty(() => Password).ObservesProperty(()=> Email);
            this.RegisterCommand = new DelegateCommand(this.Register);

        }

    

        private void Register()
        {
            throw new NotImplementedException();
        }

        private bool CanLogin()
        {
            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
            {
                return true;
            }
            return false;
        }

        private void Login()
        {
            //Comunicar com API

            //For testing
            if (this.Email == "manel" && this.Password == "manel")
            {
                this._pageDialogService.DisplayAlert("Login Successfully", "Welcome " + this.Email, "Ok");
                this._navigationService.Navigate("MasterView/NavigationView/HomeView");
            }
            else
            {
                this._pageDialogService.DisplayAlert("Login Failed", "Email and/or Password incorrect", "Ok");
            }
        }
    }
}
