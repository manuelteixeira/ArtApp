﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using ArtApp.Database;
using ArtApp.Model;
using Prism.Navigation;
using Prism.Services;

namespace ArtApp.ViewModels
{
    public class CreateTodoItemViewModel : BindableBase
    {

        #region Services

        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;
        private readonly TodoDatabase _todoDatabase;

        #endregion

        #region Properties

        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _notes;

        public string Notes
        {
            get { return _notes; }
            set { SetProperty(ref _notes, value); }
        }

        private bool _done;

        public bool Done
        {
            get { return _done; }
            set { SetProperty(ref _done, value); }
        }

        #endregion

        #region Commands

        public DelegateCommand CreateTodoItemCommand { get; private set; }

        #endregion


        public CreateTodoItemViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;
            _todoDatabase = new TodoDatabase();

            this.CreateTodoItemCommand = new DelegateCommand(CreateTodoItem);

        }

        #region Command Methods

        private void CreateTodoItem()
        {
            TodoItem todoItem = new TodoItem()
            {
                Name = this.Name,
                Notes = this.Notes,
                Done = this.Done,
            };

            if (_todoDatabase.SaveTodoItem(todoItem) == 0)
            {
                _pageDialogService.DisplayAlert("Todo Item", "The todo item wasn't created, please try again later",
                    "Ok");
                _navigationService.Navigate("TodoItemsView");
            }

            _pageDialogService.DisplayAlert("Todo Item", "The todo item was created", "Ok");
            _navigationService.Navigate("TodoItemsView");

        }

        #endregion
    }
}