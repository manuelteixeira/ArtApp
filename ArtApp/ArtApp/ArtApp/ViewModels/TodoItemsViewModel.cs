using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArtApp.Database;
using ArtApp.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace ArtApp.ViewModels
{
    public class TodoItemsViewModel : BindableBase
    {

        #region Services
        private IPageDialogService _pageDialogService;
        private INavigationService _navigationService;
        private readonly TodoDatabase _todoDatabase;
        #endregion


        #region Properties


        private IEnumerable<TodoItem> _todoItems;
        public IEnumerable<TodoItem> TodoItems
        {
            get { return _todoItems; }
            set { SetProperty(ref _todoItems, value); }
        }
        #endregion

        #region Command
        public DelegateCommand CreateTodoItemCommand { get; private set; }
        #endregion


        public TodoItemsViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;

            _todoDatabase = new TodoDatabase();

            this.CreateTodoItemCommand = new DelegateCommand(CreateTodoItem);
            GetTodoItems();
        }

        private void GetTodoItems()
        {

            TodoItems = _todoDatabase.GetItems();
        }

        #region Command Methods
        private void CreateTodoItem()
        {
            this._navigationService.Navigate("CreateTodoItemView");
        }
        #endregion
    }
}
