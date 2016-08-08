using Prism.Commands;
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
    public class DetailsTodoItemViewModel : BindableBase, INavigationAware
    {


        #region Services
        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;
        private TodoDatabase _todoDatabase;
        #endregion

        #region Properties
        private int ID { get; set; }

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
        public DelegateCommand EditTodoItemCommand { get; private set; }
        public DelegateCommand DeleteTodoItemCommand { get; private set; }
        public DelegateCommand DisplayTodoItemActionSheetCommand { get; private set; }
        #endregion

        public DetailsTodoItemViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            _todoDatabase = new TodoDatabase();

            this.CreateTodoItemCommand = new DelegateCommand(CreateTodoItem);
            this.EditTodoItemCommand = new DelegateCommand(EditTodoItem);
            this.DeleteTodoItemCommand = new DelegateCommand(DeleteTodoItem);
            this.DisplayTodoItemActionSheetCommand = new DelegateCommand(DisplayTodoItemActionSheet);
        }

        #region Command Methods
        private async void DisplayTodoItemActionSheet()
        {
            IActionSheetButton AddAction = ActionSheetButton.CreateButton("Add", this.CreateTodoItemCommand);
            IActionSheetButton EditAction = ActionSheetButton.CreateButton("Edit", this.EditTodoItemCommand);
            IActionSheetButton DeleteAction = ActionSheetButton.CreateButton("Delete", this.DeleteTodoItemCommand);
            IActionSheetButton CancelAction = ActionSheetButton.CreateCancelButton("Cancel", new DelegateCommand(() => _navigationService.GoBack()));

            await
                this._pageDialogService.DisplayActionSheet("Todo Item Actions", AddAction, EditAction,
                    DeleteAction, CancelAction);
        }

        private async void DeleteTodoItem()
        {
            var result = await this._pageDialogService.DisplayAlert("Todo Item", "Are you sure you want to delete this todo item?", "Confirm",
    "Cancel");

            if (result == true) //the user confirms 
            {
                if (this._todoDatabase.DeleteItem(this.ID) == 0)
                {
                    await this._pageDialogService.DisplayAlert("Condition Report", "Condition Report wasn't deleted", "ok");
                    await this._navigationService.Navigate("TodoItemsView");
                }
                await this._pageDialogService.DisplayAlert("Condition Report", "Condition Report was deleted successfully", "ok");
                await this._navigationService.Navigate("TodoItemsView");
            }
        }

        private void EditTodoItem()
        {
            var parameters = new NavigationParameters();
            parameters.Add("id", this.ID);
            this._navigationService.Navigate("EditTodoItemView", parameters);
        }

        private void CreateTodoItem()
        {
            this._navigationService.Navigate("CreateTodoItemView");
        } 
        #endregion

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("id"))
            {
                TodoItem todoItem = _todoDatabase.GetItem((int) parameters["id"]);

                this.ID = todoItem.ID;
                this.Name = todoItem.Name;
                this.Notes = todoItem.Notes;
                this.Done = todoItem.Done;
            }
        }
    }
}
