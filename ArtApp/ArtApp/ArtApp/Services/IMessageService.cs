using System.Threading.Tasks;

namespace ArtApp.Services
{
    public interface IMessageService
    {
        Task ShowASync(string message);
    }
}
