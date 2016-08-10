using Prism.Mvvm;
using System;
using ArtApp.Model;
using ArtApp.Repositories;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace ArtApp.ViewModels
{
    public class DetailsConditionReportViewModel : BindableBase, INavigationAware
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

        private Handling _handling;
        public Handling Handling
        {
            get { return _handling; }
            set { SetProperty(ref _handling, value); }
        }

        private HandlingPosition _handlingPosition;
        public HandlingPosition HandlingPosition
        {
            get { return _handlingPosition; }
            set { SetProperty(ref _handlingPosition, value); }
        }

        private Protection _frontProtection;
        public Protection FrontProtection
        {
            get { return _frontProtection; }
            set { SetProperty(ref _frontProtection, value); }
        }

        private Protection _backProtection;
        public Protection BackProtection
        {
            get { return _backProtection; }
            set { SetProperty(ref _backProtection, value); }
        }

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

        #endregion

        #region Commands
        public DelegateCommand CreateConditionReportCommand { get; private set; }
        public DelegateCommand EditConditionReportCommand { get; private set; }
        public DelegateCommand DeleteConditionReportCommand { get; private set; }
        public DelegateCommand DisplayConditionReportActionSheetCommand { get; private set; } 
        #endregion

        public DetailsConditionReportViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {        
            //For API objects
            //this._conditionReportRepository = new ConditionReportRepository();
            this._conditionReportMockRepository = new ConditionReportMockRepository();

            this._pageDialogService = pageDialogService;
            this._navigationService = navigationService;

            this.EditConditionReportCommand = new DelegateCommand(this.EditConditionReport);
            this.DeleteConditionReportCommand = new DelegateCommand(this.DeleteConditionReport);
            this.CreateConditionReportCommand = new DelegateCommand(this.CreateConditionReport);
            this.DisplayConditionReportActionSheetCommand = new DelegateCommand(this.DisplayConditionReportActionSheet);

        }

        #region Command Methods
        private async void DisplayConditionReportActionSheet()
        {
            IActionSheetButton AddAction = ActionSheetButton.CreateButton("Add", this.CreateConditionReportCommand);
            IActionSheetButton EditAction = ActionSheetButton.CreateButton("Edit", this.EditConditionReportCommand);
            IActionSheetButton DeleteAction = ActionSheetButton.CreateButton("Delete", this.DeleteConditionReportCommand);
            IActionSheetButton CancelAction = ActionSheetButton.CreateCancelButton("Cancel", new DelegateCommand(() => _navigationService.GoBack()));

            await
                this._pageDialogService.DisplayActionSheet("Condition Reports Actions", AddAction, EditAction,
                    DeleteAction, CancelAction);
        }

        private void CreateConditionReport()
        {
            this._navigationService.Navigate("CreateConditionReportView");
        }

        private async void DeleteConditionReport()
        {
            var result = await this._pageDialogService.DisplayAlert("Condition Report", "Are you sure you want to delete this condition report?", "Confirm",
                "Cancel");

            if (result == true) //the user confirms 
            {
                //await this._conditionReportRepository.DeleteConditionReportAsync(this.Id);
                //Confirm if was delete see StatusCode?
                this._pageDialogService.DisplayAlert("Condition Report", "Condition Report was deleted successfully", "ok");
                this._navigationService.GoBack();
                //Force conditionReportlist refresh?
            }
        }

        private void EditConditionReport()
        {
            var parameters = new NavigationParameters();
            parameters.Add("id", this.ConditionReportId);
            this._navigationService.Navigate("EditConditionReportView", parameters);
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
                this.Handling = conditionReport.Handling.Value;
                this.HandlingPosition = conditionReport.HandlingPosition.Value;
                this.FrontProtection = conditionReport.FrontProtection.Value;
                this.BackProtection = conditionReport.BackProtection.Value;
                this.Date = conditionReport.Date;
                this.MadeBy = conditionReport.MadeBy;
                this.Notes = conditionReport.Notes;
                this.Work = conditionReport.Work;


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
