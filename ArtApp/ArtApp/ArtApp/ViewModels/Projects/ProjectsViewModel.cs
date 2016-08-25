using System;
using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ArtApp.Controls;
using ArtApp.Model;
using ArtApp.Repositories;
using Prism.Commands;
using Prism.Services;
using ProjectRepository = ArtApp.Repositories.Database.ProjectRepository;

namespace ArtApp.ViewModels
{
    public class ProjectsViewModel : BindableBase
    {
        #region Services
        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;

        //For API objects
        //protected readonly ProjectRepository _projectRepository;
        //For mock objects
        protected readonly ProjectMockRepository _projectMockRepository;
        private readonly ProjectRepository _projectRepository;
        #endregion


        #region Properties
        private bool _isBusy;
        public bool isBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }


        private ObservableCollection<Project> _projectSearch;
        public ObservableCollection<Project> ProjectSearch
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

        private Project _projectSelected;
        public Project ProjectSelected
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

        private List<Project> Projects { get; set; }

        private IEnumerable<GroupList<char, Project>> _projectsGroupList;
        public IEnumerable<GroupList<char, Project>> ProjectssGroupList
        {
            get { return _projectsGroupList; }
            set { SetProperty(ref _projectsGroupList, value); }
        }
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
            this._projectRepository = new ProjectRepository();

            this.SearchProjectCommand = new DelegateCommand(this.SearchProject);
            this.CreateProjectCommand = new DelegateCommand(this.CreateProject);
            this.DetailsProjectCommand = new DelegateCommand(this.DetailsProject);
            this.RefreshProjectListCommand = new DelegateCommand(this.GetProjects);
            //creating the projects list
            this.GetProjects();
        }

        #region Command Methods
        private void GetProjects()
        {
            if (this.isBusy)
            {
                return;
            }

            this.isBusy = true;

            this.Projects = this._projectRepository.GetProjects().ToList();

            this.ProjectSearch = new ObservableCollection<Project>(Projects);
            this.CreateProjectGroup();

            this.isBusy = false;
        }

        private void DetailsProject()
        {
            var parameters = new NavigationParameters();
            parameters.Add("id", this.ProjectSelected.Id);
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
                this.ProjectSearch = new ObservableCollection<Project>(Projects);
                this.CreateProjectGroup();
            }
            else
            {
                //Pesquisa pelo titulo
                //Oferecer outras opçoes de pesquisa? autor? data?
                this.ProjectSearch = new ObservableCollection<Project>
                    (Projects.FindAll(p => p.Name.ToLower().Contains(this.SearchText.ToLower())));
                this.CreateProjectGroup();
            }
        }

        private void CreateProjectGroup()
        {
            IEnumerable<Project> projects = this.ProjectSearch;
            this.ProjectssGroupList = from project in projects
                                  orderby project.Name
                                  group project by project.Name[0]
                into groups
                                  select new GroupList<char, Project>(Char.ToUpper(groups.Key), groups);
        }

        #endregion
    }
}
