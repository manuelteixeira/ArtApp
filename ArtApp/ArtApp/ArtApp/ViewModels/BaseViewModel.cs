using System.ComponentModel;
using System.Runtime.CompilerServices;
using ArtApp.Annotations;
using Xamarin.Forms;

namespace ArtApp.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        protected readonly Services.IMessageService _messageService;
        protected readonly Services.INavigationService _navigationService;

        public event PropertyChangedEventHandler PropertyChanged;

        protected BaseViewModel()
        {
            //Procura quem implementa a interface
            this._messageService = DependencyService.Get<Services.IMessageService>();
            this._navigationService = DependencyService.Get<Services.INavigationService>();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
