using Prism.Mvvm;
using System;
using System.Collections.Generic;
using ArtApp.Model;
using ArtApp.Repositories;
using ArtApp.Repositories.Database;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace ArtApp.ViewModels
{
    public class DetailsProjectViewModel : BindableBase, INavigationAware
    {

        #region Services
        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;

        //For API objects
        //private readonly ProjectRepository _projectRepository; 

        //For mock objects
        private readonly ProjectMockRepository _projectMockRepository;
        private readonly Repositories.Database.ProjectRepository _projectRepository;
        #endregion

        #region Properties

        private int Id { get; set; }
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

        private ICollection<Work> _works;
        public ICollection<Work> Works
        {
            get { return _works; }
            set { SetProperty(ref _works, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand CreateProjectCommand { get; private set; }
        public DelegateCommand EditProjectCommand { get; private set; }
        public DelegateCommand DeleteProjectCommand { get; private set; }
        public DelegateCommand DisplayProjectActionSheetCommand { get; private set; }
        #endregion

        public DetailsProjectViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            //For API objects
            //this._projectRepository = new ProjectRepository();
            this._projectMockRepository = new ProjectMockRepository();
            this._projectRepository = new Repositories.Database.ProjectRepository();

            this._pageDialogService = pageDialogService;
            this._navigationService = navigationService;

            this.EditProjectCommand = new DelegateCommand(this.EditProject);
            this.DeleteProjectCommand = new DelegateCommand(this.DeleteProject);
            this.CreateProjectCommand = new DelegateCommand(this.CreateProject);
            this.DisplayProjectActionSheetCommand = new DelegateCommand(this.DisplayProjectActionSheet);

        }

        #region Command Methods
        private async void DisplayProjectActionSheet()
        {
            IActionSheetButton AddAction = ActionSheetButton.CreateButton("Add", this.CreateProjectCommand);
            IActionSheetButton EditAction = ActionSheetButton.CreateButton("Edit", this.EditProjectCommand);
            IActionSheetButton DeleteAction = ActionSheetButton.CreateButton("Delete", this.DeleteProjectCommand);
            IActionSheetButton CancelAction = ActionSheetButton.CreateCancelButton("Cancel", new DelegateCommand(() => _navigationService.GoBack()));

            await
                this._pageDialogService.DisplayActionSheet("Projects Actions", AddAction, EditAction,
                    DeleteAction, CancelAction);
        }

        private void CreateProject()
        {
            this._navigationService.Navigate("CreateProjectView");
        }

        private async void DeleteProject()
        {
            var result = await this._pageDialogService.DisplayAlert("Project", "Are you sure you want to delete this project?", "Confirm",
                "Cancel");

            if (result == true) //the user confirms 
            {
                if (this._projectRepository.DeleteProject(this.Id) != 0)
                {
                    await this._pageDialogService.DisplayAlert("Project", "Project was deleted successfully", "ok");
                    await this._navigationService.GoBack();
                    //Force worklist refresh?
                }
                else
                {
                    await this._pageDialogService.DisplayAlert("Project", "Failed to delete", "ok");
                    await this._navigationService.GoBack();
                }
            }
        }

        private void EditProject()
        {
            var parameters = new NavigationParameters();
            parameters.Add("id", this.Id);
            this._navigationService.Navigate("EditProjectView", parameters);
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
        }
    }
}
