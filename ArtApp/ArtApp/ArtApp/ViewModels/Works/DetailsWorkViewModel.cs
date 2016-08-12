using System;
using System.Collections.Generic;
using ArtApp.Model;
using Prism.Mvvm;
using ArtApp.Repositories;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace ArtApp.ViewModels
{
    public class DetailsWorkViewModel : BindableBase, INavigationAware
    {
        #region Services
        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;
        private readonly WorkRepository _workRepository;
        private readonly Repositories.Database.WorkRepository _workDatabase;
        #endregion

        #region Properties
        //Need to be completed with the rest of the work attributes
        private int Id { get; set; }

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

        private string _photoPath;
        public string PhotoPath
        {
            get { return _photoPath; }
            set { SetProperty(ref _photoPath, value); }
        }

        private IEnumerable<Author> _authors;
        public IEnumerable<Author> Authors
        {
            get { return _authors; }
            set { SetProperty(ref _authors, value); }
        }

        #endregion

        #region Commands

        public DelegateCommand CreateWorkCommand { get; private set; }
        public DelegateCommand EditWorkCommand { get; private set; }
        public DelegateCommand DeleteWorkCommand { get; private set; }

        public DelegateCommand DisplayWorkActionSheetCommand { get; private set; } 
        #endregion

        public DetailsWorkViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this._workRepository = new WorkRepository();
            this._pageDialogService = pageDialogService;
            this._navigationService = navigationService;
            this._workDatabase = new Repositories.Database.WorkRepository();

            this.EditWorkCommand = new DelegateCommand(this.EditWork);
            this.DeleteWorkCommand = new DelegateCommand(this.DeleteWork);
            this.CreateWorkCommand = new DelegateCommand(this.CreateWork);

            this.DisplayWorkActionSheetCommand = new DelegateCommand(this.DisplayWorkActionSheet);

        }

        #region Command Methods
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

        private async void DeleteWork()
        {
            var result = await this._pageDialogService.DisplayAlert("Work", "Are you sure you want to delete this work?", "Confirm",
                "Cancel");

            if (result == true) //the user confirms 
            {
                if (this._workDatabase.DeleteWork(this.Id) != 0)
                {
                    await this._pageDialogService.DisplayAlert("Work", "Work was deleted successfully", "ok");
                    await this._navigationService.GoBack();
                    //Force worklist refresh?
                }
                else
                {
                    await this._pageDialogService.DisplayAlert("Work", "Failed to delete", "ok");
                    await this._navigationService.GoBack();
                }
            }

        }

        private void EditWork()
        {
            var parameters = new NavigationParameters();
            parameters.Add("id", this.Id);
            this._navigationService.Navigate("EditWorkView", parameters);
        }

        private void CreateWork()
        {
            this._navigationService.Navigate("CreateWorkView");
        } 
        #endregion

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("id"))
            {
                Work work = _workDatabase.GetWork((int) parameters["id"]);

                this.Id = work.Id;
                this.Title = work.Title;
                this.Description = work.Description;
                this.Authors = work.Authors;
                this.PhotoPath = work.PhotoPath;

                //Pedir ao repositorio API
                //Work work = new Work();
                //work = this._workRepository.GetWorkAsync((string)parameters["id"]).Result;
                ////update attributes
                //this._workId = work.Id;
                //this.Title = work.Title;
                //this.Description = work.Description;
            }
        }
    }
}
