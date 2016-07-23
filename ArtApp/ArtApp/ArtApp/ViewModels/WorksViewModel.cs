using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ArtApp.Repositories;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace ArtApp.ViewModels
{
    public class WorksViewModel : BindableBase
    {
        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;
        protected readonly WorkRepository _workRepository;

        private ObservableCollection<WorkViewModel> _worksSearch;
        public ObservableCollection<WorkViewModel> WorksSearch
        {
            get { return _worksSearch; }
            set { SetProperty(ref _worksSearch, value); }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                SetProperty(ref _searchText, value);

            }
        }

        private WorkViewModel _workSelected;
        public WorkViewModel WorkSelected
        {
            get { return _workSelected; }
            set
            {
                if (value != null)
                {
                    SetProperty(ref _workSelected, value);
                    DetailsWork();
                }
            }
        }

        private List<WorkViewModel> Works { get; set; }

        public DelegateCommand SearchWorkCommand { get; private set; }
        public DelegateCommand CreateWorkCommand { get; private set; }
        public DelegateCommand DetailsWorkCommand { get; private set; }

        public WorksViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this._navigationService = navigationService;
            this._pageDialogService = pageDialogService;
            this._workRepository = new WorkRepository();

            this.SearchWorkCommand = new DelegateCommand(this.SearchWork);
            this.CreateWorkCommand = new DelegateCommand(this.CreateWork);
            this.DetailsWorkCommand = new DelegateCommand(this.DetailsWork);
            //creating the works list
            this.GetWorks();

        }



        #region Command method
        private void SearchWork()
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

        private async void GetWorks()
        {
            this.Works = new List<WorkViewModel>();
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
                        //... the rest of the needed attributes
                    });
                }

            }

            this.WorksSearch = new ObservableCollection<WorkViewModel>(Works);
        }

        private void DetailsWork()
        {
            var parameters = new NavigationParameters();
            parameters.Add("id", this.WorkSelected.Id);
            this._navigationService.Navigate("DetailsWorkView", parameters);
        }

        private void CreateWork()
        {
            this._navigationService.Navigate("CreateWorkView");

        }
        #endregion
    }
}
