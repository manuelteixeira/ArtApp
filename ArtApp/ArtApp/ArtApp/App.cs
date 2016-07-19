using ArtApp.Models;
using ArtApp.Views;
using Xamarin.Forms;

namespace ArtApp
{
    public class App : Application
    {

        public App()
        {

            #region Services DependencyInjection
            DependencyService.Register<Services.IMessageService, Services.MessageService>();
            DependencyService.Register<Services.INavigationService, Services.NavigationService>();
            //DependencyService.Register<Services.IRestApiService<Work,string>, Services.RestApiService<Work,string>>();
            #endregion

            // The root page of your application
            MainPage = new NavigationPage(new LoginView());
            //MainPage = new MenuPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
