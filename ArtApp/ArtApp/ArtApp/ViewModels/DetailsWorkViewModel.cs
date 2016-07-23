using System;
using Prism.Mvvm;
using ArtApp.Repositories;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace ArtApp.ViewModels
{
    public class DetailsWorkViewModel : BindableBase, INavigationAware
    {
        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;
        private readonly WorkRepository _workRepository;

        #region Properties
        //Need to be completed with the rest of the work attributes
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        } 
        #endregion

        public DelegateCommand CreateWorkCommand { get; private set; }
        public DelegateCommand EditWorkCommand { get; private set; }
        public DelegateCommand DeleteWorkCommand { get; private set; }

        public DelegateCommand DisplayWorkActionSheetCommand { get; private set; }
        public DelegateCommand GoBack { get; private set; } 

        public DetailsWorkViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this._workRepository = new WorkRepository();
            this._pageDialogService = pageDialogService;
            this._navigationService = navigationService;

            this.EditWorkCommand = new DelegateCommand(this.EditWork);
            this.DeleteWorkCommand = new DelegateCommand(this.DeleteWork);
            this.CreateWorkCommand = new DelegateCommand(this.CreateWork);

            this.DisplayWorkActionSheetCommand = new DelegateCommand(this.DisplayWorkActionSheet);
            this.GoBack = new DelegateCommand(() => this._navigationService.GoBack());

        }

        private async void DisplayWorkActionSheet()
        {
            IActionSheetButton AddAction = ActionSheetButton.CreateButton("Add", this.CreateWorkCommand);
            IActionSheetButton EditAction = ActionSheetButton.CreateButton("Edit", this.EditWorkCommand);
            IActionSheetButton DeleteAction = ActionSheetButton.CreateButton("Delete", this.DeleteWorkCommand);
            IActionSheetButton CancelAction = ActionSheetButton.CreateCancelButton("Cancel", new DelegateCommand(() => _navigationService.GoBack()));

            await
                this._pageDialogService.DisplayActionSheet("Work Actions", AddAction, EditAction,
                    DeleteAction, CancelAction);
        }

        private void DeleteWork()
        {
            throw new NotImplementedException();
        }

        private void EditWork()
        {
            this._navigationService.Navigate("EditWorkView");
        }

        private void CreateWork()
        {
            this._navigationService.Navigate("CreateWorkView");

        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("id"))
            {
               //Pedir ao repositorio API
            }
        }
    }
}
