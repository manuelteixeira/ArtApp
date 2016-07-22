using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ArtApp.Repositories;
using Prism.Navigation;
using Prism.Services;

namespace ArtApp.ViewModels
{
    public class WorksViewModel : BindableBase
    {
        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;
        protected readonly WorkRepository _workRepository;

        private ObservableCollection<WorkViewModel> _worksSearch;       
        public ObservableCollection<WorkViewModel> WorksSearch
        {
            get { return _worksSearch; }
            set { SetProperty(ref _worksSearch, value); }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        private List<WorkViewModel> Works { get; set; }

        public DelegateCommand SearchWorkCommand { get; private set; }
        public DelegateCommand CreateWorkCommand { get; private set; }
        public DelegateCommand EditWorkCommand { get; private set; }
        public DelegateCommand DeleteWorkCommand { get; private set; }
        public DelegateCommand DetailsWorkCommand { get; private set; }
        public DelegateCommand DisplayWorkActionSheetCommand { get; private set; } 

        public WorksViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this._navigationService = navigationService;
            this._pageDialogService = pageDialogService;
            this._workRepository = new WorkRepository();

            this.SearchWorkCommand = new DelegateCommand(this.SearchWork);
            this.CreateWorkCommand = new DelegateCommand(this.CreateWork);
            this.EditWorkCommand = new DelegateCommand(this.EditWork);
            this.DeleteWorkCommand = new DelegateCommand(this.DeleteWork);
            this.DetailsWorkCommand = new DelegateCommand(this.DetailsWork);
            this.DisplayWorkActionSheetCommand = new DelegateCommand(this.DisplayWorkActionSheet);
            //creating the works list
            this.GetWorks();

        }

        private async void DisplayWorkActionSheet()
        {
            IActionSheetButton DetailsAction = ActionSheetButton.CreateButton("Detail", this.DetailsWorkCommand);
            IActionSheetButton AddAction = ActionSheetButton.CreateButton("Add", this.CreateWorkCommand);
            IActionSheetButton EditAction = ActionSheetButton.CreateButton("Edit", this.EditWorkCommand);
            IActionSheetButton DeleteAction = ActionSheetButton.CreateButton("Detail", this.DeleteWorkCommand);
            IActionSheetButton CancelAction = ActionSheetButton.CreateCancelButton("Cancel", new DelegateCommand(() => _navigationService.GoBack()));

            await
                this._pageDialogService.DisplayActionSheet("Work Actions", AddAction, DetailsAction, EditAction,
                    DeleteAction);
        }

        private void DetailsWork()
        {
            throw new NotImplementedException();
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

        #region Command method
        private void SearchWork()
        {
            this.WorksSearch.Clear();

            if (string.IsNullOrEmpty(this.SearchText))
            {
                this.WorksSearch = new ObservableCollection<WorkViewModel>(Works);
            }
            else
            {
                this.WorksSearch = new ObservableCollection<WorkViewModel>
                    (Works.FindAll(p => p.Title.ToLower().Contains(this.SearchText.ToLower())));
            }
        }

        private async void GetWorks()
        {
            this.Works = new List<WorkViewModel>();
            var list = await this._workRepository.GetWorksAsync();
            //Populate the list in the mainview
            foreach (var item in list)
            {
                if (!string.IsNullOrEmpty(item.Title))
                {
                    Works.Add(new WorkViewModel()
                    {
                        Title = item.Title,
                        Description = item.Description,
                        //... the rest of the needed attributes
                    });
                }

            }

            this.WorksSearch = new ObservableCollection<WorkViewModel>(Works);
        } 
        #endregion
    }
}
