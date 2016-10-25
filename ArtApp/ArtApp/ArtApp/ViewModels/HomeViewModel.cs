using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using ArtApp.Model;
using ArtApp.Repositories.Database;
using Prism.Navigation;
using Prism.Services;

namespace ArtApp.ViewModels
{
    public class HomeViewModel : BindableBase
    {

        #region Services
        private IPageDialogService _pageDialogService;
        private INavigationService _navigationService;
        private readonly TodoItemRepository _todoRepository;
        private readonly ProjectRepository _projectRepository;
        #endregion

        #region Properties
        private bool _isBusy;
        public bool isBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private TodoItem _todoItemSelected;
        public TodoItem TodoItemSelected
        {
            get { return _todoItemSelected; }
            set
            {

                if (value != null)
                {
                    SetProperty(ref _todoItemSelected, value);
                    DetailsTodoItem();

                }
            }
        }

        private IEnumerable<TodoItem> _todoItems;
        public IEnumerable<TodoItem> TodoItems
        {
            get { return _todoItems; }
            set { SetProperty(ref _todoItems, value); }
        }


        private IEnumerable<Project> _projects;
        public IEnumerable<Project> Projects
        {
            get { return _projects; }
            set { SetProperty(ref _projects, value); }
        }
        #endregion

        #region Command
        public DelegateCommand ResfreshTodoItemsListCommand { get; private set; }
        public DelegateCommand DetailsTodoItemCommand { get; private set; }
        #endregion


        public HomeViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;

            _todoRepository = new TodoItemRepository();
            _projectRepository = new ProjectRepository();

            this.ResfreshTodoItemsListCommand = new DelegateCommand(GetTodoItems);
            this.DetailsTodoItemCommand = new DelegateCommand(DetailsTodoItem);

            GetTodoItems();
            GetProjects();
        }



        #region Command Methods
        private void GetTodoItems()
        {
            if (this.isBusy)
            {
                return;
            }

            this.isBusy = true;

            TodoItems = _todoRepository.GetTodoItems();

            this.isBusy = false;
        }

        private void DetailsTodoItem()
        {
            var parameters = new NavigationParameters();
            parameters.Add("id", this.TodoItemSelected.Id);
            this._navigationService.Navigate("DetailsTodoItemView", parameters);
        }

        private void GetProjects()
        {
            if (this.isBusy)
            {
                return;
            }

            this.isBusy = true;

            Projects = _projectRepository.GetProjects();

            this.isBusy = false;
        }
        #endregion
    }
}
