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
    public class DetailsPathologyViewModel : BindableBase, INavigationAware
    {
        #region Services

        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;
        private readonly PathologyRepository _pathologyRepository;

        #endregion

        #region Properties

        private int Id { get; set; }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string _icon;

        public string Icon
        {
            get { return _icon; }
            set { SetProperty(ref _icon, value); }
        }

        #endregion

        #region Commands

        public DelegateCommand CreatePathologyCommand { get; private set; }
        public DelegateCommand EditPathologyCommand { get; private set; }
        public DelegateCommand DeletePathologyCommand { get; private set; }

        public DelegateCommand DisplayPathologyActionSheetCommand { get; private set; }

        #endregion

        public DetailsPathologyViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this._pageDialogService = pageDialogService;
            this._navigationService = navigationService;
            this._pathologyRepository = new PathologyRepository();

            this.EditPathologyCommand = new DelegateCommand(this.EditPathology);
            this.DeletePathologyCommand = new DelegateCommand(this.DeletePathology);
            this.CreatePathologyCommand = new DelegateCommand(this.CreatePathology);

            this.DisplayPathologyActionSheetCommand = new DelegateCommand(this.DisplayPathologyActionSheet);
        }

        private async void DisplayPathologyActionSheet()
        {
            IActionSheetButton AddAction = ActionSheetButton.CreateButton("Add", this.CreatePathologyCommand);
            IActionSheetButton EditAction = ActionSheetButton.CreateButton("Edit", this.EditPathologyCommand);
            IActionSheetButton DeleteAction = ActionSheetButton.CreateButton("Delete", this.DeletePathologyCommand);
            IActionSheetButton CancelAction = ActionSheetButton.CreateCancelButton("Cancel", new DelegateCommand(() => _navigationService.GoBack()));

            await
                this._pageDialogService.DisplayActionSheet("Pathology Actions", AddAction, EditAction,
                    DeleteAction, CancelAction);
        }

        private void CreatePathology()
        {
            this._navigationService.Navigate("CreatePathologyView");
        }

        private async void DeletePathology()
        {
            var result = await this._pageDialogService.DisplayAlert("Pathology", "Are you sure you want to delete this pathology?", "Confirm",
                "Cancel");

            if (result == true) //the user confirms 
            {
                if (this._pathologyRepository.DeletePathology(this.Id) != 0)
                {
                    await this._pageDialogService.DisplayAlert("Pathology", "Pathology was deleted successfully", "ok");
                    await this._navigationService.GoBack();
                    //Force worklist refresh?
                }
                else
                {
                    await this._pageDialogService.DisplayAlert("Pathology", "Failed to delete", "ok");
                    await this._navigationService.GoBack();
                }
            }
        }

        private void EditPathology()
        {
            var parameters = new NavigationParameters();
            parameters.Add("id", this.Id);
            this._navigationService.Navigate("EditPathologyView", parameters);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("id"))
            {
                Pathology pathology = _pathologyRepository.GetPathology((int)parameters["id"]);

                this.Id = pathology.Id;
                this.Name = pathology.Name;
                this.Description = pathology.Description;
                this.Icon = pathology.Icon;
            }
        }
    }
}