using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using ArtApp.Model;
using ArtApp.Repositories;
using Prism.Navigation;
using Prism.Services;

namespace ArtApp.ViewModels
{
    public class CreateWorkViewModel : BindableBase
    {
        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;
        private readonly WorkRepository _workRepository;

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

        public DelegateCommand CreateWorkCommand { get; private set; } 

        public CreateWorkViewModel(IPageDialogService pageDialogService, INavigationService navigationService)
        {
            this._workRepository = new WorkRepository();
            this._pageDialogService = pageDialogService;
            this._navigationService = navigationService;

            this.CreateWorkCommand = new DelegateCommand(CreateWork);

        }

        private async void CreateWork()
        {
            Work work = new Work()
            {
                Title = this.Title,
                Description = this.Description,
                //The rest of the work attributes 
            };

            if (await this._workRepository.PostWorkAsync(work) != null)
            {
                await this._pageDialogService.DisplayAlert("Work", "New work created", "Ok");
                this._navigationService.GoBack();
            }
            else
            {
                await this._pageDialogService.DisplayAlert("Work", "Failed to create work", "Ok");
            }
        }

    }
}
