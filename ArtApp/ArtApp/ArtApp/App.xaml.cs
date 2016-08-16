using ArtApp.Model;
using ArtApp.Services.Interfaces;
using ArtApp.ViewModels;
using Prism.Unity;
using ArtApp.Views;
using SQLite.Net;
using Xamarin.Forms;

namespace ArtApp
{
    public partial class App : PrismApplication
    {
        protected override void OnInitialized()
        {
            InitializeComponent();


            #region CLEAN DB - REMOVE AFTER TESTING!
            SQLiteConnection database = DependencyService.Get<ISQLite>().GetConnection();
            database.DropTable<Work>();
            database.DropTable<Author>();
            database.DropTable<WorkAuthor>();
            database.DropTable<Classification>();
            database.DropTable<ArtType>();
            database.DropTable<ConditionReport>();
            database.DropTable<Pathology>();
            database.DropTable<ConditionReportPathology>();
            database.DropTable<ConditionReportPhoto>();
            database.DropTable<Photo>();
            #endregion

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
            Container.RegisterTypeForNavigation<CreateTodoItemView>();
            Container.RegisterTypeForNavigation<EditTodoItemView>();
            Container.RegisterTypeForNavigation<DetailsTodoItemView>();
            Container.RegisterTypeForNavigation<CreatePathologyView>();
            Container.RegisterTypeForNavigation<PathologiesView>();


        }
    }
}
