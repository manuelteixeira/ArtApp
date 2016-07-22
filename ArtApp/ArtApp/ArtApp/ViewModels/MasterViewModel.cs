using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;

namespace ArtApp.ViewModels
{
    public class MasterViewModel : BindableBase, INavigationAware
    {
        private INavigationService _navigationService;

        public DelegateCommand<string> NavigateCommand { get; private set; } 

        public MasterViewModel(INavigationService navigationService)
        {
            this._navigationService = navigationService;
            this.NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string name)
        {
            _navigationService.Navigate(name);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {

        }
    }
}
