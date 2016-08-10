using Prism.Commands;
using Prism.Mvvm;
using ArtApp.Database;
using ArtApp.Model;
using ArtApp.Repositories.Database;
using Prism.Navigation;
using Prism.Services;

namespace ArtApp.ViewModels
{
    public class EditTodoItemViewModel : BindableBase, INavigationAware
    {

        #region Services

        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;
        private readonly TodoItemRepository _todoRepository;

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

        public DelegateCommand EditTodoItemCommand { get; private set; }

        #endregion

        public EditTodoItemViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            _todoRepository = new TodoItemRepository();
            

            this.EditTodoItemCommand = new DelegateCommand(EditTodoItem);

        }

        #region Command Methods
        private async void EditTodoItem()
        {
            TodoItem todoItem = new TodoItem()
            {
                Id = this.ID,
                Name = this.Name,
                Notes = this.Notes,
                Done = this.Done
            };

            if (this._todoRepository.SaveTodoItem(todoItem) == 0)
            {
                await this._pageDialogService.DisplayAlert("Todo Item", "The todo item wasn't edited", "Ok");
                await this._navigationService.Navigate("TodoItemsView");
            }

            await this._pageDialogService.DisplayAlert("Todo Item", "The " + this.Name + " was edited successfully", "Ok");
            await this._navigationService.Navigate("TodoItemsView");

        }
        #endregion

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("id"))
            {
                TodoItem todoItem = _todoRepository.GetTodoItem((int)parameters["id"]);

                this.ID = todoItem.Id;
                this.Name = todoItem.Name;
                this.Notes = todoItem.Notes;
                this.Done = todoItem.Done;
            }
        }
    }
}
