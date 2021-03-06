﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ArtApp.Controls;
using ArtApp.Model;
using Prism.Mvvm;
using ArtApp.Repositories;
using ArtApp.Repositories.Database;
using Microsoft.Practices.Unity.ObjectBuilder;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using ProjectRepository = ArtApp.Repositories.Database.ProjectRepository;
using WorkRepository = ArtApp.Repositories.Database.WorkRepository;

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
        private readonly ProjectRepository _projectRepository;
        private readonly WorkRepository _workRepository;
        private readonly TodoItemRepository _todoItemRepository;
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
        public DelegateCommand CreateProjectCommand { get; private set; }
        #endregion

        public CreateProjectViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            //For API objects
            //this._projectRepository = new ProjectRepository();
            //For mock objects
            this._projectMockRepository = new ProjectMockRepository();
            this._projectRepository = new ProjectRepository();
            this._workRepository = new WorkRepository();
            //for task automation
            this._todoItemRepository = new TodoItemRepository();
            

            this._pageDialogService = pageDialogService;
            this._navigationService = navigationService;

            this.CreateProjectCommand = new DelegateCommand(CreateProject);

            this.GetWorks();

            //default date value
            this.BeginDate = DateTime.Now;
            this.EndDate = DateTime.Now;

        }

        private void GetSelectedWorks()
        {
            this.Works = WorkItems.Where(p => p.IsSelected).Select(p => p.Item).ToList();
        }

        private void GetWorks()
        {
            this.WorkItems = new ObservableCollection<SelectableItemWrapper<Work>>(this._workRepository.GetWorks().Select(work => new SelectableItemWrapper<Work>() { Item = work }));
        }

        #region Command methods
        private async void CreateProject()
        {
            GetSelectedWorks();

            Project project = new Project()
            {
                Name = this.Name,
                BeginDate = this.BeginDate,
                EndDate = this.EndDate,
                Works = this.Works
            };

            CreateTasksOfWorks();


            //For API objects
            //if (await this._conditionReportRepository.PostConditionReportAsync(conditionReport) != null)
            if (this._projectRepository.SaveProject(project) != 0)
            {
                await this._pageDialogService.DisplayAlert("Project", "New project created", "Ok");
                await this._navigationService.GoBack();
            }
            else
            {
                await this._pageDialogService.DisplayAlert("Project", "Failed to create project", "Ok");
            }
        }

        private void CreateTasksOfWorks()
        {
            foreach (var work in Works)
            {
                TodoItem todoitem = new TodoItem()
                {
                    Name = "Create CR of " + work.Title,
                    Notes = "From the project " +  this.Name,
                    Done = false
                };

                this._todoItemRepository.SaveTodoItem(todoitem);
            }
        }

        #endregion
    }
}
