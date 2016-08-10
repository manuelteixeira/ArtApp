using System.Collections.Generic;
using ArtApp.Database;
using Prism.Commands;
using Prism.Mvvm;
using ArtApp.Model;
using ArtApp.Repositories;
using Prism.Navigation;
using Prism.Services;

namespace ArtApp.ViewModels
{
    public class CreateWorkViewModel : BindableBase
    {
        #region Services
        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;
        private readonly WorkRepository _workRepository;
        private readonly Repositories.Database.WorkRepository _workDatabase;
        #endregion

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

        private string _authorName;
        public string AuthorName
        {
            get { return _authorName; }
            set { SetProperty(ref _authorName, value); }
        }


        #endregion

        #region Commands
        public DelegateCommand CreateWorkCommand { get; private set; }
        #endregion


        public CreateWorkViewModel(IPageDialogService pageDialogService, INavigationService navigationService)
        {
            this._workRepository = new WorkRepository();
            this._pageDialogService = pageDialogService;
            this._navigationService = navigationService;
            this._workDatabase = new Repositories.Database.WorkRepository();

            this.CreateWorkCommand = new DelegateCommand(CreateWork);

        }

        #region Command methods
        //private async void CreateWork()
        //{
        //    Work work = new Work()
        //    {
        //        Title = this.Title,
        //        Description = this.Description,
        //        //The rest of the work attributes 
        //    };

        //    if (await this._workRepository.PostWorkAsync(work) != null)
        //    {
        //        await this._pageDialogService.DisplayAlert("Work", "New work created", "Ok");
        //        this._navigationService.GoBack();
        //    }
        //    else
        //    {
        //        await this._pageDialogService.DisplayAlert("Work", "Failed to create work", "Ok");
        //    }
        //}

        private async void CreateWork()
        {

            Work work = new Work()
            {
                Title = this.Title,
                Description = this.Description,
                Authors = new List<Author>()
                {
                    new Author()
                    {
                        Name = this.AuthorName
                    }
                }
            };


            if (_workDatabase.SaveWork(work) != 0)
            {
                await this._pageDialogService.DisplayAlert("Work", "The work was created successfully", "ok");
                await this._navigationService.GoBack();
            }
            else
            {
                await this._pageDialogService.DisplayAlert("Work", "The work failed to create", "ok");
                await this._navigationService.GoBack();
            }


        }

        #endregion
    }
}
