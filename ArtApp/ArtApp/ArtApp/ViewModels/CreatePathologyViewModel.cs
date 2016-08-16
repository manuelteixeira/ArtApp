using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ArtApp.Model;
using ArtApp.Repositories.Database;
using Prism.Navigation;
using Prism.Services;
using WorkRepository = ArtApp.Repositories.WorkRepository;

namespace ArtApp.ViewModels
{
    public class CreatePathologyViewModel : BindableBase
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

        #endregion

        public CreatePathologyViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {

            this._navigationService = navigationService;
            this._pageDialogService = pageDialogService;
            this._pathologyRepository = new PathologyRepository();
            
            this.CreatePathologyCommand = new DelegateCommand(CreatePathology);

        }

        #region Command methods
        private async void CreatePathology()
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
    }
}
