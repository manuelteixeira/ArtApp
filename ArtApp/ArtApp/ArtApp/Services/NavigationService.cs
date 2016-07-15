using System.Threading.Tasks;
using ArtApp.Views;

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
            await ArtApp.App.Current.MainPage.Navigation.PushAsync(new MainView());
        }

        public async Task NavigateToRegister()
        {
            await ArtApp.App.Current.MainPage.Navigation.PushAsync(new RegisterView());
        }
    }
}
