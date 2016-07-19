using System;
using System.Linq;
using System.Threading.Tasks;
using ArtApp.Views;
using Xamarin.Forms;

namespace ArtApp.Services
{
    public class NavigationService : INavigationService
    {

        public async Task NavigateToLogin()
        {
            await ArtApp.App.Current.MainPage.Navigation.PushAsync(new LoginView());
        }

        public async Task NavigateToMain()
        {
            ((MasterDetailPage)ArtApp.App.Current.MainPage).Detail = new NavigationPage(new MainView());
            ((MasterDetailPage)ArtApp.App.Current.MainPage).IsPresented = false;
            //await ArtApp.App.Current.MainPage.Navigation.PushAsync(new MainView());
        }

        public async Task NavigateToMaster()
        {
            await ArtApp.App.Current.MainPage.Navigation.PushAsync(new MasterView());
        }

        public void SetMainPage()
        {
            ArtApp.App.Current.MainPage = new MasterView();
        }

        public async Task NavigateToWorks()
        {
            ((MasterDetailPage)ArtApp.App.Current.MainPage).Detail = new NavigationPage(new WorksView());
            ((MasterDetailPage)ArtApp.App.Current.MainPage).IsPresented = false;
            //await ArtApp.App.Current.MainPage.Navigation.PushAsync(new WorksView());
        }

        public async Task NavigateToConditionReports()
        {
            ((MasterDetailPage)ArtApp.App.Current.MainPage).Detail = new NavigationPage(new ConditionReportsView());
            ((MasterDetailPage)ArtApp.App.Current.MainPage).IsPresented = false;
        }

        public Task NavigateToSettings()
        {
            throw new NotImplementedException();
        }

        public async Task NavigateToRegister()
        {
            await ArtApp.App.Current.MainPage.Navigation.PushAsync(new RegisterView());
        }

        public async Task NavigateToWork()
        {
            await ((MasterDetailPage) ArtApp.App.Current.MainPage).Detail.Navigation.PushAsync(new WorkView());
            //await ArtApp.App.Current.MainPage.Navigation.PushAsync(new WorkView());
        }




    }
}
