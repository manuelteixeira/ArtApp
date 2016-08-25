using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ArtApp.Controls;
using ArtApp.Model;
using ArtApp.Repositories.Database;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace ArtApp.ViewModels
{
    public class PathologiesViewModel : BindableBase
    {

        #region Services
        protected readonly IPageDialogService _pageDialogService;
        protected readonly INavigationService _navigationService;
        private readonly PathologyRepository _pathologyRepository;
        #endregion


        #region Properties
        private bool _isBusy;
        public bool isBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }


        private ObservableCollection<Pathology> _pathologiesSearch;
        public ObservableCollection<Pathology> PathologiesSearch
        {
            get { return _pathologiesSearch; }
            set { SetProperty(ref _pathologiesSearch, value); }
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

        private Pathology _pathologySelected;
        public Pathology PathologySelected
        {
            get { return _pathologySelected; }
            set
            {
                if (value != null)
                {
                    SetProperty(ref _pathologySelected, value);
                    DetailsPathology();
                }
            }
        }

        private List<Pathology> Pathologies { get; set; }

        private ObservableCollection<SelectableItemWrapper<Pathology>> _pathologiesA;
        public ObservableCollection<SelectableItemWrapper<Pathology>> PathologiesA
        {
            get { return _pathologiesA; }
            set { SetProperty(ref _pathologiesA, value); }
        }

        private ObservableCollection<Pathology> _selectedPathologiesA;
        public ObservableCollection<Pathology> SelectedPathologiesA
        {
            get { return _selectedPathologiesA ?? new ObservableCollection<Pathology>(); }
            set
            {

                SetProperty(ref _selectedPathologiesA, value);

            }
        }

        private IEnumerable<GroupList<char, Pathology>> _pathologiesGroupList;
        public IEnumerable<GroupList<char, Pathology>> PathologiesGroupList
        {
            get { return _pathologiesGroupList; }
            set { SetProperty(ref _pathologiesGroupList, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand SearchPathologyCommand { get; private set; }
        public DelegateCommand CreatePathologyCommand { get; private set; }
        public DelegateCommand DetailsPathologyCommand { get; private set; }
        public DelegateCommand ResfreshPathologyListCommand { get; private set; }
        public DelegateCommand GoBackCommand { get; private set; }
        #endregion


        public PathologiesViewModel(IPageDialogService pageDialogService, INavigationService navigationService)
        {
            this._navigationService = navigationService;
            this._pageDialogService = pageDialogService;
            this._pathologyRepository = new PathologyRepository();

            this.CreatePathologyCommand = new DelegateCommand(this.CreatePathology);
            this.DetailsPathologyCommand = new DelegateCommand(this.DetailsPathology);
            this.SearchPathologyCommand = new DelegateCommand(this.SearchPathology);
            this.ResfreshPathologyListCommand = new DelegateCommand(this.GetPathologies);
            this.GoBackCommand = new DelegateCommand(this.GoBack);

            this.PathologiesA = new ObservableCollection<SelectableItemWrapper<Pathology>>(this._pathologyRepository.GetPathologies().ToList().Select(pathology => new SelectableItemWrapper<Pathology>() { Item = pathology }));

            this.GetPathologies();
        }

        private void GoBack()
        {
            this._navigationService.GoBack();
        }

        #region Command Methods
        private void CreatePathology()
        {
            this._navigationService.Navigate("CreatePathologyView");
        }

        private void DetailsPathology()
        {
            var parameters = new NavigationParameters();
            parameters.Add("id", this.PathologySelected.Id);
            this._navigationService.Navigate("DetailsPathologyView", parameters);
        }

        private void SearchPathology()
        {
            this.PathologiesSearch.Clear();

            if (string.IsNullOrEmpty(this.SearchText))
            {
                this.PathologiesSearch = new ObservableCollection<Pathology>(Pathologies);
                this.CreatePathologiesGroup();
            }
            else
            {
                this.PathologiesSearch = new ObservableCollection<Pathology>
                    (Pathologies.FindAll(p => p.Name.ToLower().Contains(this.SearchText.ToLower())));
                this.CreatePathologiesGroup();

            }
        }

        private void GetPathologies()
        {
            Pathologies = new List<Pathology>();
            if (this.isBusy)
            {
                return;
            }

            this.isBusy = true;

            Pathologies = _pathologyRepository.GetPathologies().ToList();

            this.PathologiesSearch = new ObservableCollection<Pathology>(Pathologies);
            this.CreatePathologiesGroup();

            this.isBusy = false;
        }

        private void CreatePathologiesGroup()
        {
            IEnumerable<Pathology> pathologies = this.PathologiesSearch;
            this.PathologiesGroupList = from pathology in pathologies
                                        orderby pathology.Name
                                        group pathology by pathology.Name[0]
                                  into groups
                                        select new GroupList<char, Pathology>(Char.ToUpper(groups.Key), groups);
        }
        #endregion


    }
}
