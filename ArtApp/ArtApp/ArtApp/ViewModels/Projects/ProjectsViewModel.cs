using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArtApp.Repositories;
using Prism.Commands;
using Prism.Services;

namespace ArtApp.ViewModels
{
    public class ProjectsViewModel : BindableBase
    {
        #region Properties
        private bool _isBusy;
        public bool isBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;

        //For API objects
        //protected readonly ProjectRepository _projectRepository;
        //For mock objects
        protected readonly ProjectMockRepository _projectMockRepository;


        private ObservableCollection<ProjectViewModel> _projectSearch;
        public ObservableCollection<ProjectViewModel> ProjectSearch
        {
            get { return _projectSearch; }
            set { SetProperty(ref _projectSearch, value); }
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

        private ProjectViewModel _projectSelected;
        public ProjectViewModel ProjectSelected
        {
            get { return _projectSelected; }
            set
            {
                if (value != null)
                {
                    SetProperty(ref _projectSelected, value);
                    DetailsProject();
                }
            }
        }

        private List<ProjectViewModel> Projects { get; set; }
        #endregion

        #region Commands
        public DelegateCommand SearchProjectCommand { get; private set; }
        public DelegateCommand CreateProjectCommand { get; private set; }
        public DelegateCommand DetailsProjectCommand { get; private set; }
        public DelegateCommand RefreshProjectListCommand { get; private set; }
        #endregion

        public ProjectsViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this._navigationService = navigationService;
            this._pageDialogService = pageDialogService;

            //For API objects
            //this._projectRepository = new ProjectRepository();
            this._projectMockRepository = new ProjectMockRepository();

            this.SearchProjectCommand = new DelegateCommand(this.SearchProject);
            this.CreateProjectCommand = new DelegateCommand(this.CreateProject);
            this.DetailsProjectCommand = new DelegateCommand(this.DetailsProject);
            this.RefreshProjectListCommand = new DelegateCommand(this.GetProjects);

            //creating the projects list
            this.GetProjects();
        }

        #region Command Methods
        private async void GetProjects()
        {
            if (this.isBusy)
            {
                return;
            }

            this.isBusy = true;

            this.Projects = new List<ProjectViewModel>();

            //For API objects
            //var list = await this._projectRepository.GetProjectsAsync();
            var list = await this._projectMockRepository.GetProjectsAsync();

            //Populate the list in the mainview
            foreach (var item in list)
            {
                //Talvez nºao seja necessario verificar o null, a API verifica.
                if (!string.IsNullOrEmpty(item.Name))
                {
                    Projects.Add(new ProjectViewModel()
                    {
                        ProjectId = item.ProjectId,
                        Name = item.Name,
                        BeginDate = item.BeginDate,
                        EndDate = item.EndDate,
                        Works = item.Works
                        //... the rest of the needed attributes
                    });
                }

            }

            this.ProjectSearch = new ObservableCollection<ProjectViewModel>(Projects);
            this.isBusy = false;
        }

        private void DetailsProject()
        {
            var parameters = new NavigationParameters();
            parameters.Add("id", this.ProjectSelected.ProjectId);
            this._navigationService.Navigate("DetailsProjectView", parameters);
        }

        private void CreateProject()
        {
            this._navigationService.Navigate("CreateProjectView");
        }

        private void SearchProject()
        {
            this.ProjectSearch.Clear();

            if (string.IsNullOrEmpty(this.SearchText))
            {
                this.ProjectSearch = new ObservableCollection<ProjectViewModel>(Projects);
            }
            else
            {
                //Pesquisa pelo titulo
                //Oferecer outras opçoes de pesquisa? autor? data?
                this.ProjectSearch = new ObservableCollection<ProjectViewModel>
                    (Projects.FindAll(p => p.Name.ToLower().Contains(this.SearchText.ToLower())));
            }
        } 
        #endregion
    }
}
