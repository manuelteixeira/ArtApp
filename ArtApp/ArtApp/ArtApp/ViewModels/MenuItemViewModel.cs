using System.Dynamic;
using System.Windows.Input;
using Xamarin.Forms;

namespace ArtApp.ViewModels
{
    public class MenuItemViewModel : BaseViewModel
    {
        #region Properties
        public string Icon { get; set; }
        public string Title { get; set; }
        public string PageName { get; set; }
        #endregion

        #region Commands
        public ICommand NavigateCommand { get; set; }
        #endregion




        public MenuItemViewModel()
        {
            this.NavigateCommand = new Command<string>(this.Navigate);

        }

        private async void Navigate(string pageName)
        {
            switch (pageName)
            {
                case "home":
                    await this._navigationService.NavigateToMain();
                    break;
                case "works":
                    await this._navigationService.NavigateToWorks();
                    //await this._navigationService.NavigateToWorks();
                    break;
                case "conditionreports":
                    await this._navigationService.NavigateToConditionReports();
                    break;
                case "settings":
                    await this._navigationService.NavigateToSettings();
                    break;
                default:
                    break;
            }
        }

    }

}
