using System;
using System.Collections.ObjectModel;
using ArtApp.Model;
using ArtApp.Repositories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace ArtApp.ViewModels
{
    public class EditConditionReportViewModel : BindableBase, INavigationAware
    {

        #region Services
        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;
        
        //For API objects
        //private readonly ConditionReportRepository _conditionReportRepository; 
        //For mock objects
        private readonly ConditionReportMockRepository _conditionReportMockRepository;
        #endregion

        #region Properties
        //Need to be completed with the rest of the condition Report attributes
        private int ConditionReportId { get; set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private float _rh;
        public float Rh
        {
            get { return _rh; }
            set { SetProperty(ref _rh, value); }
        }

        private float _lux;
        public float Lux
        {
            get { return _lux; }
            set { SetProperty(ref _lux, value); }
        }

        private float _temperature;
        public float Temperature
        {
            get { return _temperature; }
            set { SetProperty(ref _temperature, value); }
        }

        //private Handling _handling;
        //public Handling Handling
        //{
        //    get { return _handling; }
        //    set { SetProperty(ref _handling, value); }
        //}

        //private HandlingPosition _handlingPosition;
        //public HandlingPosition HandlingPosition
        //{
        //    get { return _handlingPosition; }
        //    set { SetProperty(ref _handlingPosition, value); }
        //}

        //private Protection _frontProtection;
        //public Protection FrontProtection
        //{
        //    get { return _frontProtection; }
        //    set { SetProperty(ref _frontProtection, value); }
        //}

        //private Protection _backProtection;
        //public Protection BackProtection
        //{
        //    get { return _backProtection; }
        //    set { SetProperty(ref _backProtection, value); }
        //}

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }

        private string _madeBy;
        public string MadeBy
        {
            get { return _madeBy; }
            set { SetProperty(ref _madeBy, value); }
        }

        private string _notes;
        public string Notes
        {
            get { return _notes; }
            set { SetProperty(ref _notes, value); }
        }

        private Work _work;
        public Work Work
        {
            get { return _work; }
            set { SetProperty(ref _work, value); }
        }

        //Pickers Data
        private ObservableCollection<string> _handlingOptions;
        public ObservableCollection<string> HandlingOptions
        {
            get { return _handlingOptions; }
            set { SetProperty(ref _handlingOptions, value); }
        }

        private ObservableCollection<string> _handlingPositionsOptions;
        public ObservableCollection<string> HandlingPositionsOptions
        {
            get { return _handlingPositionsOptions; }
            set { SetProperty(ref _handlingPositionsOptions, value); }
        }

        private ObservableCollection<string> _protectionOptions;
        public ObservableCollection<string> ProtectionOptions
        {
            get { return _protectionOptions; }
            set { SetProperty(ref _protectionOptions, value); }
        }

        #endregion


        #region Commands

        public DelegateCommand EditConditionReportCommand { get; private set; }

        #endregion


        public EditConditionReportViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this._navigationService = navigationService;
            this._pageDialogService = pageDialogService;
            //For API objects
            //this._conditionReportRepository = new ConditionReportRepository();
            this._conditionReportMockRepository = new ConditionReportMockRepository();

            this.EditConditionReportCommand = new DelegateCommand(this.EditConditionReport);
            
            //Populate Pickers
            PopulatePickers();
        }

        private void PopulatePickers()
        {
            //this.HandlingOptions = new ObservableCollection<string>(Enum.GetNames(typeof(Model.Handling)));
            //this.HandlingPositionsOptions = new ObservableCollection<string>(Enum.GetNames(typeof(Model.HandlingPosition)));
            //this.ProtectionOptions = new ObservableCollection<string>(Enum.GetNames(typeof(Model.Protection)));
        }

        #region Command Methods

        private async void EditConditionReport()
        {
            ConditionReport conditionReport = new ConditionReport()
            {
                Title = this.Title,
                RH = this.Rh,
                Lux = this.Lux,
                Temperature = this.Temperature,
                //Handling = this.Handling,
                //HandlingPosition = this.HandlingPosition,
                //FrontProtection = this.FrontProtection,
                //BackProtection = this.BackProtection,
                Date = this.Date,
                MadeBy = this.MadeBy,
                Notes = this.Notes,
                //Work = this.Work,
            };

            if (await this._conditionReportMockRepository.PutConditionReportAsync(
                        conditionReport.Id.ToString(), conditionReport) != null)
            {
                await this._pageDialogService.DisplayAlert("Condition Report",
                    "Condition Report edited: New Title: " + conditionReport.Title, "Ok");
            }
            else
            {
                await
                    this._pageDialogService.DisplayAlert("Condition Report", "Failed to edit the condition report", "Ok");
            }
            await this._navigationService.GoBack();

            ////IMPLEMENTAR
            //if (await this._conditionReportRepository.PutConditionReportAsync(conditionReport.Id.ToString(), conditionReport) != null)
            //{
            //    await this._pageDialogService.DisplayAlert("Condition Report", "Condition Report edited", "Ok");
            //    this._navigationService.GoBack();
            //}
            //else
            //{
            //    await this._pageDialogService.DisplayAlert("Condition Report", "Failed to edit condition report", "Ok");
            //}
        }

        #endregion

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("id"))
            {
                //Mock objects
                ConditionReport conditionReport =
                    await this._conditionReportMockRepository.GetConditionReportAsync((int)parameters["id"]);

                this.ConditionReportId = conditionReport.Id;
                this.Title = conditionReport.Title;
                this.Rh = conditionReport.RH;
                this.Lux = conditionReport.Lux;
                this.Temperature = conditionReport.Temperature;
                //this.Handling = conditionReport.Handling.Value;
                //this.HandlingPosition = conditionReport.HandlingPosition.Value;
                //this.FrontProtection = conditionReport.FrontProtection.Value;
                //this.BackProtection = conditionReport.BackProtection.Value;
                this.Date = conditionReport.Date;
                this.MadeBy = conditionReport.MadeBy;
                this.Notes = conditionReport.Notes;
                //this.Work = conditionReport.Work;


                ////Pedir ao repositorio API
                //ConditionReport conditionReport = new ConditionReport();
                //conditionReport = this._conditionReportRepository.GetConditionReportAsync((string)parameters["id"]).Result;
                ////update attributes
                //this.Id = conditionReport.Id;
                //this.Title = ConditionReport.Title;
            }
        }
    }
}
