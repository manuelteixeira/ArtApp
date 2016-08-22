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
    public class EditPathologyViewModel : BindableBase, INavigationAware
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
        public DelegateCommand EditPathologyCommand { get; private set; }

        #endregion

        public EditPathologyViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this._pageDialogService = pageDialogService;
            this._navigationService = navigationService;
            this._pathologyRepository = new PathologyRepository();

            this.EditPathologyCommand = new DelegateCommand(EditPathology);
        }

        #region Command Methods
        private async void EditPathology()
        {
            Pathology pathology = new Pathology()
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                Icon = this.Icon,
            };


            if (_pathologyRepository.SavePathology(pathology) != 0)
            {
                await this._pageDialogService.DisplayAlert("Pathology", "The pathology was created successfully", "ok");
                await this._navigationService.GoBack();
            }
            else
            {
                await this._pageDialogService.DisplayAlert("Pathology", "The pathology failed to create", "ok");
                await this._navigationService.GoBack();
            }
        }

        #endregion

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
