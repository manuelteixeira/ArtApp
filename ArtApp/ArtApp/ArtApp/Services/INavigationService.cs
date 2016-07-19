using System.Threading.Tasks;

namespace ArtApp.Services
{
    public interface INavigationService
    {

        //Login and Register Navigation
        Task NavigateToLogin();
        Task NavigateToRegister();


        Task NavigateToMain();
        Task NavigateToMaster();
        void SetMainPage();

        //Menu Navigation
        Task NavigateToWorks();
        Task NavigateToConditionReports();
        Task NavigateToSettings();

        //Works
        Task NavigateToWork();

    }
}
