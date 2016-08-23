using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ArtApp.Controls;
using ArtApp.Model;
using ArtApp.Repositories;
using Prism.Navigation;
using Prism.Services;
using ProjectRepository = ArtApp.Repositories.Database.ProjectRepository;

namespace ArtApp.ViewModels
{
    public class EditProjectViewModel : BindableBase, INavigationAware
    {

        #region Services
        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;

        //For API objects
        //private readonly ProjectRepository _projectRepository; 
        //For mock objects
        private readonly ProjectMockRepository _projectMockRepository;
        private readonly ProjectRepository _projectRepository;
        private readonly Repositories.Database.WorkRepository _workRepository;

        #endregion

        #region Properties

        private int Id;

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private DateTime _beginDate;
        public DateTime BeginDate
        {
            get { return _beginDate; }
            set { SetProperty(ref _beginDate, value); }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get { return _endDate; }
            set { SetProperty(ref _endDate, value); }
        }

        private List<Work> _works;
        public List<Work> Works
        {
            get { return _works; }
            set { SetProperty(ref _works, value); }
        }

        private ObservableCollection<SelectableItemWrapper<Work>> _workItems;
        public ObservableCollection<SelectableItemWrapper<Work>> WorkItems
        {
            get { return _workItems; }
            set { SetProperty(ref _workItems, value); }
        }
        #endregion

        #region Commands

        public DelegateCommand EditProjectCommand { get; private set; }

        #endregion

        public EditProjectViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this._navigationService = navigationService;
            this._pageDialogService = pageDialogService;

            //For API objects
            //this._projectRepository = new ProjectRepository();
            this._projectMockRepository = new ProjectMockRepository();
            this._projectRepository = new ProjectRepository();
            this._workRepository = new Repositories.Database.WorkRepository();


            this.EditProjectCommand = new DelegateCommand(this.EditProject);
        }

        #region Command Methods

        private async void EditProject()
        {
            GetSelectedWorks();

            Project project = new Project()
            {
                Id = this.Id,
                Name = this.Name,
                BeginDate = this.BeginDate,
                EndDate = this.EndDate,
                Works = this.Works,
            };

            if (this._projectRepository.SaveProject(project) != 0)
            {
                await this._pageDialogService.DisplayAlert("Project",
                    "Project edited: New Name: " + project.Name, "Ok");
            }
            else
            {
                await
                    this._pageDialogService.DisplayAlert("Project", "Failed to edit the project", "Ok");
            }
            await this._navigationService.GoBack();

        }

        private void GetWorks()
        {
            this.WorkItems = new ObservableCollection<SelectableItemWrapper<Work>>(this._workRepository.GetWorks().Select(work => new SelectableItemWrapper<Work>() { Item = work }));
            this.ShowActualWorks();

        }

        private void ShowActualWorks()
        {
            foreach (var work in WorkItems)
            {
                if (Works.Exists(p => p.Title == work.Item.Title))
                {
                    work.IsSelected = true;
                }
            }
        }

        private void GetSelectedWorks()
        {
            this.Works = WorkItems.Where(p => p.IsSelected).Select(p => p.Item).ToList();
        }

        #endregion

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("id"))
            {
                //Mock objects
                Project project = this._projectRepository.GetProject((int)parameters["id"]);

                this.Id = project.Id;
                this.Name = project.Name;
                this.BeginDate = project.BeginDate;
                this.EndDate = project.EndDate;
                this.Works = project.Works;
            }

            //Works
            GetWorks();
        }
    }
}
