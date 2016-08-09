﻿using System.Collections.Generic;
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
        #endregion

        #region Command
        public DelegateCommand CreateTodoItemCommand { get; private set; }
        public DelegateCommand ResfreshTodoItemsListCommand { get; private set; }
        public DelegateCommand DetailsTodoItemCommand { get; private set; }
        #endregion


        public TodoItemsViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;

            _todoDatabase = new TodoDatabase();

            this.CreateTodoItemCommand = new DelegateCommand(CreateTodoItem);
            this.ResfreshTodoItemsListCommand = new DelegateCommand(GetTodoItems);
            this.DetailsTodoItemCommand = new DelegateCommand(DetailsTodoItem);
            GetTodoItems();
        }


        #region Command Methods
        private void CreateTodoItem()
        {
            this._navigationService.Navigate("CreateTodoItemView");
        }

        private void GetTodoItems()
        {
            if (this.isBusy)
            {
                return;
            }

            this.isBusy = true;

            TodoItems = _todoDatabase.GetItems();

            this.isBusy = false;
        }

        private void DetailsTodoItem()
        {
            var parameters = new NavigationParameters();
            parameters.Add("id", this.TodoItemSelected.ID);
            this._navigationService.Navigate("DetailsTodoItemView", parameters);
        }

        #endregion
    }
}