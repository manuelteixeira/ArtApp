using System;
using System.Collections.Generic;
using ArtApp.Model;
using Prism.Mvvm;
using ArtApp.Repositories;
using Microsoft.Practices.Unity.ObjectBuilder;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace ArtApp.ViewModels
{
    public class CreateProjectViewModel : BindableBase
    {

        #region Services
        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;

        //For API objects
        //private readonly ProjectRepository _projectRepository;
        //For Mock Objects
        private readonly ProjectMockRepository _projectMockRepository;
        #endregion

        #region Properties
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
        #endregion

        public CreateProjectViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            //For API objects
            //this._projectRepository = new ProjectRepository();
            //For mock objects
            this._projectMockRepository = new ProjectMockRepository();

            this._pageDialogService = pageDialogService;
            this._navigationService = navigationService;

            this.CreateProjectCommand = new DelegateCommand(CreateProject);

        }


        #region Command methods
        private async void CreateProject()
        {
            Project project = new Project()
            {

            };


            //For API objects
            //if (await this._conditionReportRepository.PostConditionReportAsync(conditionReport) != null)
            if (await this._projectMockRepository.PostProjectAsync(project) != null)
            {
                await this._pageDialogService.DisplayAlert("Project", "New project created", "Ok");
                await this._navigationService.GoBack();
            }
            else
            {
                await this._pageDialogService.DisplayAlert("Project", "Failed to create project", "Ok");
            }
        }
        #endregion
    }
}
