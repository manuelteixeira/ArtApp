using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using ArtApp.Model;
using ArtApp.Repositories;
using Prism.Navigation;
using Prism.Services;

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

        public DelegateCommand EditProjectCommand { get; private set; }

        #endregion

        public EditProjectViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this._navigationService = navigationService;
            this._pageDialogService = pageDialogService;

            //For API objects
            //this._projectRepository = new ProjectRepository();
            this._projectMockRepository = new ProjectMockRepository();

            this.EditProjectCommand = new DelegateCommand(this.EditProject);
        }

        #region Command Methods

        private async void EditProject()
        {
            Project project = new Project()
            {
                Name = this.Name,
                BeginDate = this.BeginDate,
                EndDate = this.EndDate,
                Works = this.Works,
            };

            if (await this._projectMockRepository.PutProjectAsync(
                        project.ProjectId.ToString(), project) != null)
            {
                await this._pageDialogService.DisplayAlert("Project",
                    "Project edited: New Name: " + project.Name, "Ok");
            }
            else
            {
                await
                    this._pageDialogService.DisplayAlert("Project", "Failed to edit the project", "Ok");
            }
            this._navigationService.GoBack();

            ////IMPLEMENTAR
            //if (await this._conditionReportRepository.PutConditionReportAsync(conditionReport.ConditionReportId.ToString(), conditionReport) != null)
            //{
            //    await this._pageDialogService.DisplayAlert("Condition Report", "Condition Report edited", "Ok");
            //    this._navigationService.GoBack();
            //}
            //else
            //{
            //    await this._pageDialogService.DisplayAlert("Condition Report", "Failed to edit condition report", "Ok");
            //}
        }

        #endregion

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("id"))
            {
                //Mock objects
                Project project =
                    await this._projectMockRepository.GetProjectAsync((int)parameters["id"]);

                this.Name = project.Name;
                this.BeginDate = project.BeginDate;
                this.EndDate = project.EndDate;
                this.Works = project.Works;


                ////Pedir ao repositorio API
                //ConditionReport conditionReport = new ConditionReport();
                //conditionReport = this._conditionReportRepository.GetConditionReportAsync((string)parameters["id"]).Result;
                ////update attributes
                //this.ConditionReportId = conditionReport.ConditionReportId;
                //this.Title = ConditionReport.Title;
            }
        }
    }
}
