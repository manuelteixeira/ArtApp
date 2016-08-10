using System.Collections.Generic;
using System.Linq;
using ArtApp.Model;
using ArtApp.Repositories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace ArtApp.ViewModels
{
    public class EditWorkViewModel : BindableBase, INavigationAware
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

        private IEnumerable<Author> _authors;
        public IEnumerable<Author> Authors
        {
            get { return _authors; }
            set { SetProperty(ref _authors, value); }
        }

        #endregion

        #region Commands
        public DelegateCommand EditWorkCommand { get; private set; }

        #endregion


        public EditWorkViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this._navigationService = navigationService;
            this._pageDialogService = pageDialogService;
            this._workRepository = new WorkRepository();
            this._workDatabase = new Repositories.Database.WorkRepository();
            
            this.EditWorkCommand = new DelegateCommand(this.EditWork);

        }

        #region Commands Methods
        private async void EditWork()
        {
            Work work = new Work()
            {
                Id = this.Id,
                Title = this.Title,
                Description = this.Description,
                Authors = this.Authors.ToList()
                //The rest of the work attributes 
            };

            if (this._workDatabase.SaveWork(work) != 0)
            {
                await this._pageDialogService.DisplayAlert("Work", "Work edited: New Title: " + work.Title, "Ok");
                await this._navigationService.GoBack();
            }
            else
            {
                await this._pageDialogService.DisplayAlert("Work", "Work edit failed", "Ok");
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
                Work work = _workDatabase.GetWork((int)parameters["id"]);

                this.Id = work.Id;
                this.Title = work.Title;
                this.Description = work.Description;
                this.Authors = work.Authors;
            }
        }
    }
}
