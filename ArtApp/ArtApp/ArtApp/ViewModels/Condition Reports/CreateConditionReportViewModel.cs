using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArtApp.Model;
using Prism.Mvvm;
using ArtApp.Repositories;
using Plugin.Media;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace ArtApp.ViewModels
{
    public class CreateConditionReportViewModel : BindableBase
    {

        #region Services
        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;

        //For API objects
        //private readonly ConditionReportRepository _conditionReportRepository;
        //For Mock Objects
        private readonly ConditionReportMockRepository _conditionReportMockRepository; 
        #endregion


        #region Properties
        //Need to be completed with the rest of the condition Report attributes
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
        public DelegateCommand CreateConditionReportCommand { get; private set; }
        public DelegateCommand TakePhotoCommand { get; private set; }
        public DelegateCommand PickPhotoCommand { get; private set; } 
        #endregion


        public CreateConditionReportViewModel(IPageDialogService pageDialogService, INavigationService navigationService)
        {
            //For API objects
            //this._conditionReportRepository = new ConditionReportRepository();
            //For mock objects
            this._conditionReportMockRepository = new ConditionReportMockRepository();

            this._pageDialogService = pageDialogService;
            this._navigationService = navigationService;

            this.CreateConditionReportCommand = new DelegateCommand(CreateConditionReport);
            this.TakePhotoCommand = new DelegateCommand(TakePhoto);
            this.PickPhotoCommand = new DelegateCommand(PickPhoto);

            //Populate Pickers
            PopulatePickers();

        }

        private void PopulatePickers()
        {
            this.HandlingOptions = new ObservableCollection<string>(Enum.GetNames(typeof(Model.Handling)));
            this.HandlingPositionsOptions = new ObservableCollection<string>(Enum.GetNames(typeof(Model.HandlingPosition)));
            this.ProtectionOptions = new ObservableCollection<string>(Enum.GetNames(typeof(Model.Protection)));
        }

        #region Command methods
        private async void CreateConditionReport()
        {
            ConditionReport conditionReport = new ConditionReport()
            {
                Title = this.Title,
                RH = this.Rh,
                Lux = this.Lux,
                Temperature = this.Temperature,
                Handling = this.Handling,
                HandlingPosition = this.HandlingPosition,
                FrontProtection = this.FrontProtection,
                BackProtection = this.BackProtection,
                Date = this.Date,
                MadeBy = this.MadeBy,
                Notes = this.Notes,
                Work = this.Work,
            };


            //For API objects
            //if (await this._conditionReportRepository.PostConditionReportAsync(conditionReport) != null)
            if (await this._conditionReportMockRepository.PostConditionReportAsync(conditionReport) != null)
            {
                await this._pageDialogService.DisplayAlert("Condition Report", "New condition report created", "Ok");
                await this._navigationService.GoBack();
            }
            else
            {
                await this._pageDialogService.DisplayAlert("Condition Report", "Failed to create condition report", "Ok");
            }
        }

        private async void TakePhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await this._pageDialogService.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                SaveToAlbum = true
            });

            if (file == null)
                return;

            await this._pageDialogService.DisplayAlert("File Location", file.Path, "OK");
        }

        private async void PickPhoto()
        {
            await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.PickPhotoAsync();

            await this._pageDialogService.DisplayAlert("Teste", file.Path, "ok");

        }
        #endregion
    }
}
