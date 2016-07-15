using System.Collections.ObjectModel;
using System.Windows.Input;
using ArtApp.Models;
using ArtApp.Repositories;
using ArtApp.Services;
using Xamarin.Forms;

namespace ArtApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        protected readonly Repositories.WorkRepository _workRepository;

        public ObservableCollection<WorkViewModel> Works { get; set; }

        public ICommand GetWorksCommand { get; set; }

        public MainViewModel()
        {
            this._workRepository = new WorkRepository();

            Works = new ObservableCollection<WorkViewModel>();

            this.GetWorksCommand = new Command(this.GetWorks);

            //TESTING
            this.GetWorks();

        }

        private async void GetWorks()
        {
            var list = await this._workRepository.GetWorksAsync();
            Works.Clear();

            //Populate the list in the mainview
            foreach (var item in list)
            {
                Works.Add(new WorkViewModel()
                {
                    Title = item.Title,
                    Description = item.Description,
                    //Date = item.Date
                });
                
            }
            
        }
    }
}
