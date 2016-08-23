using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ArtApp.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ConditionReportRepository = ArtApp.Repositories.Database.ConditionReportRepository;

namespace ArtApp.ViewModels
{
    public class ConditionReportsViewModel : BindableBase
    {

        #region Services
        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;

        //For API objects
        //protected readonly ConditionReportRepository _conditionReportRepository;
        //For mock objects
        //protected readonly ConditionReportMockRepository _conditionReportMockRepository; 
        private readonly ConditionReportRepository _conditionReportRepository;
        #endregion

        #region Properties
        private bool _isBusy;
        public bool isBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private ObservableCollection<ConditionReport> _conditionReportSearch;
        public ObservableCollection<ConditionReport> ConditionReportSearch
        {
            get { return _conditionReportSearch; }
            set { SetProperty(ref _conditionReportSearch, value); }
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

        private ConditionReport _conditionReportSelected;
        public ConditionReport ConditionReportSelected
        {
            get { return _conditionReportSelected; }
            set
            {
                if (value != null)
                {
                    SetProperty(ref _conditionReportSelected, value);
                    DetailsConditionReport();
                }
            }
        }

        private List<ConditionReport> ConditionReports { get; set; }
        #endregion


        #region Commands
        public DelegateCommand SearchConditionReportCommand { get; private set; }
        public DelegateCommand CreateConditionReportCommand { get; private set; }
        public DelegateCommand DetailsConditionReportCommand { get; private set; }
        public DelegateCommand ResfreshConditionReportsListCommand { get; private set; } 
        #endregion


        public ConditionReportsViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this._navigationService = navigationService;
            this._pageDialogService = pageDialogService;

            //For API objects
            //this._conditionReportRepository = new ConditionReportRepository();
            //this._conditionReportMockRepository = new ConditionReportMockRepository();
            this._conditionReportRepository = new ConditionReportRepository();

            this.SearchConditionReportCommand = new DelegateCommand(this.SearchConditionReport);
            this.CreateConditionReportCommand = new DelegateCommand(this.CreateConditionReport);
            this.DetailsConditionReportCommand = new DelegateCommand(this.DetailsConditionReport);
            this.ResfreshConditionReportsListCommand = new DelegateCommand(this.GetConditionReports);
            
            //creating the condition report list
            this.GetConditionReports();
        }

        #region Command Methods
        private void GetConditionReports()
        {
            if (this.isBusy)
            {
                return;
            }

            this.isBusy = true;

            this.ConditionReports = this._conditionReportRepository.GetConditionReports().ToList();


            this.ConditionReportSearch = new ObservableCollection<ConditionReport>(ConditionReports);
            this.isBusy = false;
        }

        private void DetailsConditionReport()
        {
            var parameters = new NavigationParameters();
            parameters.Add("id", this.ConditionReportSelected.Id);
            this._navigationService.Navigate("DetailsConditionReportView", parameters);
        }

        private void CreateConditionReport()
        {
            this._navigationService.Navigate("CreateConditionReportView");
        }

        private void SearchConditionReport()
        {
            this.ConditionReportSearch.Clear();

            if (string.IsNullOrEmpty(this.SearchText))
            {
                this.ConditionReportSearch = new ObservableCollection<ConditionReport>(ConditionReports);
            }
            else
            {
                //Pesquisa pelo titulo
                //Oferecer outras opçoes de pesquisa? autor? data?
                this.ConditionReportSearch = new ObservableCollection<ConditionReport>
                    (ConditionReports.FindAll(p => p.Title.ToLower().Contains(this.SearchText.ToLower())));
            }
        } 
        #endregion
    }
}
