﻿using System;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ArtApp.Controls;
using ArtApp.Model;
using ArtApp.Repositories;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace ArtApp.ViewModels
{
    public class WorksViewModel : BindableBase
    {


        #region Services
        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;
        protected readonly WorkRepository _workRepository;
        private readonly Repositories.Database.WorkRepository _workDatabase;
        #endregion

        #region Properties
        private bool _isBusy;
        public bool isBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }


        private ObservableCollection<Work> _worksSearch;
        public ObservableCollection<Work> WorksSearch
        {
            get { return _worksSearch; }
            set { SetProperty(ref _worksSearch, value); }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                SetProperty(ref _searchText, value);

            }
        }

        private Work _workSelected;
        public Work WorkSelected
        {
            get { return _workSelected; }
            set
            {
                if (value != null)
                {
                    SetProperty(ref _workSelected, value);
                    DetailsWork();
                }
            }
        }

        private List<Work> Works { get; set; }

        private IEnumerable<GroupList<char,Work>> _worksGroupList;
        public IEnumerable<GroupList<char, Work>> WorksGroupList
        {
            get { return _worksGroupList; }
            set { SetProperty(ref _worksGroupList, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand SearchWorkCommand { get; private set; }
        public DelegateCommand CreateWorkCommand { get; private set; }
        public DelegateCommand DetailsWorkCommand { get; private set; }
        public DelegateCommand RefreshWorkListCommand { get; private set; }  
        #endregion

        public WorksViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this._navigationService = navigationService;
            this._pageDialogService = pageDialogService;
            this._workRepository = new WorkRepository();
            this._workDatabase = new Repositories.Database.WorkRepository();

            this.SearchWorkCommand = new DelegateCommand(this.SearchWork);
            this.CreateWorkCommand = new DelegateCommand(this.CreateWork);
            this.DetailsWorkCommand = new DelegateCommand(this.DetailsWork);
            this.RefreshWorkListCommand = new DelegateCommand(this.GetWorks);
            //creating the works list
            this.GetWorks();


        }



        #region Command method
        private void SearchWork()
        {
            this.WorksSearch.Clear();

            if (string.IsNullOrEmpty(this.SearchText))
            {
                this.WorksSearch = new ObservableCollection<Work>(Works);
                this.CreateWorkGroup();
            }
            else
            {
                this.WorksSearch = new ObservableCollection<Work>
                    (Works.FindAll(p => p.Title.ToLower().Contains(this.SearchText.ToLower())));
                this.CreateWorkGroup();
            }
        }

        //private async void GetWorks()
        //{
        //    if (this.isBusy)
        //    {
        //        return;
        //    }

        //    this.isBusy = true;

        //    this.Works = new List<WorkViewModel>();
        //    var list = await this._workRepository.GetWorksAsync();
        //    //Populate the list in the mainview
        //    foreach (var item in list)
        //    {
        //        if (!string.IsNullOrEmpty(item.Title))
        //        {
        //            Works.Add(new WorkViewModel()
        //            {
        //                Title = item.Title,
        //                Description = item.Description,
        //                //... the rest of the needed attributes
        //            });
        //        }

        //    }

        //    this.WorksSearch = new ObservableCollection<WorkViewModel>(Works);
        //    this.isBusy = false;

        //}

        private void GetWorks()
        {
            Works = new List<Work>();
            if (this.isBusy)
            {
                return;
            }

            this.isBusy = true;

            Works = _workDatabase.GetWorks().ToList();

            this.WorksSearch = new ObservableCollection<Work>(Works);

            this.CreateWorkGroup();

            this.isBusy = false;
        }

        private void DetailsWork()
        {
            var parameters = new NavigationParameters();
            parameters.Add("id", this.WorkSelected.Id);
            this._navigationService.Navigate("DetailsWorkView", parameters);
        }

        private void CreateWork()
        {
            this._navigationService.Navigate("CreateWorkView");
        }

        private void CreateWorkGroup()
        {
            IEnumerable<Work> works = this.WorksSearch;
            this.WorksGroupList = from work in works
                                  orderby work.Title
                                  group work by work.Title[0]
                into groups
                                  select new GroupList<char, Work>(Char.ToUpper(groups.Key), groups);
        }
        #endregion
    }
}
