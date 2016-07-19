using System.Collections.ObjectModel;
using System.Windows.Input;
using ArtApp.Repositories;
using Xamarin.Forms;

namespace ArtApp.ViewModels
{
    public class WorksViewModel : BaseViewModel
    {
        protected readonly Repositories.WorkRepository _workRepository;

        public WorkViewModel NewWork { get; set; }
        public ObservableCollection<WorkViewModel> Works { get; set; }

        public ICommand GetWorksCommand { get; set; }
        public ICommand CreateWorkCommand { get; set; }


        public WorksViewModel()
        {
            this._workRepository = new WorkRepository();

            Works = new ObservableCollection<WorkViewModel>();

            this.GetWorksCommand = new Command(this.GetWorks);
            this.CreateWorkCommand = new Command(this.CreateWork);

            //TESTING
            this.GetWorks();
        }

        private void CreateWork()
        {
            NewWork = new WorkViewModel();
            this._navigationService.NavigateToWork();
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
