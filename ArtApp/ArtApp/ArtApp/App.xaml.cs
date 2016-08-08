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
            Container.RegisterTypeForNavigation<DetailsWorkView>();
            Container.RegisterTypeForNavigation<ConditionReportsView>();
            Container.RegisterTypeForNavigation<CreateConditionReportView>();
            Container.RegisterTypeForNavigation<EditConditionReportView>();
            Container.RegisterTypeForNavigation<DetailsConditionReportView>();
            Container.RegisterTypeForNavigation<ProjectsView>();
            Container.RegisterTypeForNavigation<CreateProjectView>();
            Container.RegisterTypeForNavigation<EditProjectView>();
            Container.RegisterTypeForNavigation<DetailsProjectView>();
            Container.RegisterTypeForNavigation<TodoItemsView>();


        }
    }
}
