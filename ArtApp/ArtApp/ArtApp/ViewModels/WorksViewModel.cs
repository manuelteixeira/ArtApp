using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
        private ObservableCollection<WorkViewModel> _worksSearch;

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

        public ObservableCollection<WorkViewModel> WorksSearch
        {
            get
            {
                return _worksSearch;
            }
            set
            {
                if (_worksSearch != value)
                {
                    _worksSearch = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private List<WorkViewModel> Works { get; set; }


        public ICommand GetWorksCommand { get; set; }
        public ICommand CreateWorkCommand { get; set; }
        public ICommand SearchWorkCommand { get; set; }


        public WorksViewModel()
        {
            this._workRepository = new WorkRepository();

            this.GetWorksCommand = new Command(this.GetWorks);
            this.CreateWorkCommand = new Command(this.CreateWork);
            this.SearchWorkCommand = new Command(this.SearchWork);

            //TESTING
            this.Works = new List<WorkViewModel>();
            this.GetWorks();
        }

        #region Command Methods
        private async void SearchWork()
        {
            this.WorksSearch.Clear();

            if (string.IsNullOrEmpty(this.SearchText))
            {
                this.WorksSearch = new ObservableCollection<WorkViewModel>(Works);
            }
            else
            {
                this.WorksSearch = new ObservableCollection<WorkViewModel>
                    (Works.FindAll(p => p.Title.ToLower().Contains(this.SearchText.ToLower())));
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
            //Populate the list in the mainview
            foreach (var item in list)
            {
                if (!string.IsNullOrEmpty(item.Title))
                {
                    Works.Add(new WorkViewModel()
                    {
                        Title = item.Title,
                        Description = item.Description,
                        //Date = item.Date
                    });
                }

            }

            this.WorksSearch = new ObservableCollection<WorkViewModel>(Works);

        } 
        #endregion

    }
}
