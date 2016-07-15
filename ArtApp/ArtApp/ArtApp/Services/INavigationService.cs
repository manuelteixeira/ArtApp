using System.Threading.Tasks;

namespace ArtApp.Services
{
    public interface INavigationService
    {
        //Baseado num dicionario String - Pagina
        //Task NavigateTo(string viewName, object param);

        Task NavigateToLogin();
        Task NavigateToRegister();
        Task NavigateToMain();
    }
}
