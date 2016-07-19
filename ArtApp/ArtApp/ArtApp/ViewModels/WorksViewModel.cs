using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ArtApp.Models;
using ArtApp.Repositories;
using Xamarin.Forms;

namespace ArtApp.ViewModels
{
    public class WorksViewModel : BaseViewModel
    {
        protected readonly Repositories.WorkRepository _workRepository;

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public WorkViewModel NewWork { get; set; }
        public ObservableCollection<WorkViewModel> Works { get; set; }
        public ObservableCollection<WorkViewModel> WorksSearch { get; set; }


        public ICommand GetWorksCommand { get; set; }
        public ICommand CreateWorkCommand { get; set; }
        public ICommand SearchWorkCommand { get; set; }


        public WorksViewModel()
        {
            this._workRepository = new WorkRepository();

            Works = new ObservableCollection<WorkViewModel>();

            this.GetWorksCommand = new Command(this.GetWorks);
            this.CreateWorkCommand = new Command(this.CreateWork);
            this.SearchWorkCommand = new Command(this.SearchWork);

            //TESTING
            this.GetWorks();
        }

        private void SearchWork()
        {
            this.WorksSearch.Clear();

            IEnumerable<WorkViewModel> foundWorks;
            if (string.IsNullOrEmpty(this.SearchText))
            {
                foundWorks = Works;
            }
            else
            {
                
                foundWorks = Works.Where(p => p.Title.ToLower().Contains(this.SearchText.ToLower()));
            }

            if (foundWorks != null)
            {
                foreach (var foundWork in foundWorks)
                {
                    this.WorksSearch.Add(foundWork);
                }
            }
            else
            {
                this.WorksSearch = new ObservableCollection<WorkViewModel>(Works);
            }
            
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

            this.WorksSearch = new ObservableCollection<WorkViewModel>(Works);

        }

    }
}
