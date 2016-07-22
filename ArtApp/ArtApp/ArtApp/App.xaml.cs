using Prism.Unity;
using ArtApp.Views;

namespace ArtApp
{
    public partial class App : PrismApplication
    {
        protected override void OnInitialized()
        {
            InitializeComponent();

            //NavigationService.Navigate("MainPage?title=Hello%20from%20Xamarin.Forms");
            NavigationService.Navigate("LoginView");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<LoginView>();
            Container.RegisterTypeForNavigation<MasterView>();
            Container.RegisterTypeForNavigation<NavigationView>();
            Container.RegisterTypeForNavigation<HomeView>();
            Container.RegisterTypeForNavigation<WorksView>();
            Container.RegisterTypeForNavigation<CreateWorkView>();
            Container.RegisterTypeForNavigation<EditWorkView>();
        }
    }
}
