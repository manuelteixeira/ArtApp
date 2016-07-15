using System.Threading.Tasks;

namespace ArtApp.Services
{
    public class MessageService : IMessageService
    {
        public async Task ShowASync(string message)
        {
            await ArtApp.App.Current.MainPage.DisplayAlert("ArtApp", message, "Ok");
        }
    }
}
