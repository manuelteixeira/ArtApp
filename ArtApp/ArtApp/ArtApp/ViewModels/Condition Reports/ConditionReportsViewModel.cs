using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArtApp.Model;
using ArtApp.Repositories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace ArtApp.ViewModels
{
    public class ConditionReportsViewModel : BindableBase
    {

        #region Properties
        private bool _isBusy;
        public bool isBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;

        //For API objects
        //protected readonly ConditionReportRepository _conditionReportRepository;
        //For mock objects
        protected readonly ConditionReportMockRepository _conditionReportMockRepository;


        private ObservableCollection<ConditionReportViewModel> _conditionReportSearch;
        public ObservableCollection<ConditionReportViewModel> ConditionReportSearch
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

        private ConditionReportViewModel _conditionReportSelected;
        public ConditionReportViewModel ConditionReportSelected
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

        private List<ConditionReportViewModel> ConditionReports { get; set; }
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
            this._conditionReportMockRepository = new ConditionReportMockRepository();

            this.SearchConditionReportCommand = new DelegateCommand(this.SearchConditionReport);
            this.CreateConditionReportCommand = new DelegateCommand(this.CreateConditionReport);
            this.DetailsConditionReportCommand = new DelegateCommand(this.DetailsConditionReport);
            this.ResfreshConditionReportsListCommand = new DelegateCommand(this.GetConditionReports);
            
            //creating the condition report list
            this.GetConditionReports();
        }

        #region Command Methods
        private async void GetConditionReports()
        {
            if (this.isBusy)
            {
                return;
            }

            this.isBusy = true;

            this.ConditionReports = new List<ConditionReportViewModel>();

            //For API objects
            //var list = await this._conditionReportRepository.GetConditionReportsAsync();
            var list = await this._conditionReportMockRepository.GetConditionReportsAsync();

            //Populate the list in the mainview
            foreach (var item in list)
            {
                //Talvez nºao seja necessario verificar o null, a API verifica.
                if (!string.IsNullOrEmpty(item.Title))
                {
                    ConditionReports.Add(new ConditionReportViewModel()
                    {
                        ConditionReportId = item.ConditionReportId,
                        Title = item.Title,
                        Date = item.Date,
                        MadeBy = item.MadeBy
                        //... the rest of the needed attributes
                    });
                }

            }

            this.ConditionReportSearch = new ObservableCollection<ConditionReportViewModel>(ConditionReports);
            this.isBusy = false;
        }

        private void DetailsConditionReport()
        {
            var parameters = new NavigationParameters();
            parameters.Add("id", this.ConditionReportSelected.ConditionReportId);
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
                this.ConditionReportSearch = new ObservableCollection<ConditionReportViewModel>(ConditionReports);
            }
            else
            {
                //Pesquisa pelo titulo
                //Oferecer outras opçoes de pesquisa? autor? data?
                this.ConditionReportSearch = new ObservableCollection<ConditionReportViewModel>
                    (ConditionReports.FindAll(p => p.Title.ToLower().Contains(this.SearchText.ToLower())));
            }
        } 
        #endregion
    }
}
