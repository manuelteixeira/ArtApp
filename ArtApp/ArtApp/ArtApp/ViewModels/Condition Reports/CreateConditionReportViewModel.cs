using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ArtApp.Controls;
using ArtApp.Model;
using Prism.Mvvm;
using ArtApp.Repositories;
using ArtApp.Repositories.Database;
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
        private readonly Repositories.Database.ConditionReportRepository _conditionReportRepository;
        private readonly PathologyRepository _pathologyRepository;
        private readonly Repositories.Database.WorkRepository _workRepository;
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

        private List<Pathology> _pathologies;
        public List<Pathology> Pathologies
        {
            get { return _pathologies; }
            set { SetProperty(ref _pathologies, value); }
        }

        private ObservableCollection<Photo> _photosPath;
        public ObservableCollection<Photo> PhotosPath
        {
            get { return _photosPath; }
            set { SetProperty(ref _photosPath, value); }
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
            set { SetProperty(ref _selectedPathologiesA, value); }
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
                }
            }
        }

        private ObservableCollection<Work> _works;
        public ObservableCollection<Work> Works
        {
            get { return _works ?? new ObservableCollection<Work>(); }
            set { SetProperty(ref _works, value); }
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
        public DelegateCommand AddPathologyCommand { get; private set; }
        public DelegateCommand SelectAllCommand { get; private set; }
        public DelegateCommand SelectNoneCommand { get; private set; }
        public DelegateCommand CreatePathologyCommand { get; private set; }
        #endregion


        public CreateConditionReportViewModel(IPageDialogService pageDialogService, INavigationService navigationService)
        {
            //For API objects
            //this._conditionReportRepository = new ConditionReportRepository();
            //For mock objects
            //this._conditionReportMockRepository = new ConditionReportMockRepository();
            this._conditionReportRepository = new Repositories.Database.ConditionReportRepository();
            this._pathologyRepository = new PathologyRepository();
            this._workRepository = new Repositories.Database.WorkRepository();

            this._pageDialogService = pageDialogService;
            this._navigationService = navigationService;

            this.CreateConditionReportCommand = new DelegateCommand(CreateConditionReport);
            this.TakePhotoCommand = new DelegateCommand(TakePhoto);
            this.PickPhotoCommand = new DelegateCommand(PickPhoto);
            this.AddPathologyCommand = new DelegateCommand(AddPathology);
            this.SelectAllCommand = new DelegateCommand(SelectAll);
            this.SelectNoneCommand = new DelegateCommand(SelectNone);
            this.CreatePathologyCommand = new DelegateCommand(this.CreatePathology);


            this.PhotosPath = new ObservableCollection<Photo>();

            //Pathologies
            GetPathologies();

            //Works
            GetWorks();

            //Populate Pickers
            PopulatePickers();

            //default date value
            this.Date = DateTime.Now;

        }

        private void GetWorks()
        {
            this.Works = new ObservableCollection<Work>(this._workRepository.GetWorks());
        }

        #region Command methods
        private async void CreateConditionReport()
        {
            GetSelectedPathologies();

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
                Pathologies = this.Pathologies.ToList(),
                Photos = this.PhotosPath.ToList(),
                Work = this.WorkSelected,
            };


            //For API objects
            //if (await this._conditionReportRepository.PostConditionReportAsync(conditionReport) != null)
            //if (await this._conditionReportMockRepository.PostConditionReportAsync(conditionReport) != null)
            if(this._conditionReportRepository.SaveConditionReport(conditionReport) != 0)
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

            this.PhotosPath.Add(new Photo() { PhotoPath = file.Path });

            await this._pageDialogService.DisplayAlert("File Location", file.Path, "OK");
        }

        private async void PickPhoto()
        {
            await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.PickPhotoAsync();

            this.PhotosPath.Add(new Photo() { PhotoPath = file.Path });

        }

        private void PopulatePickers()
        {
            this.HandlingOptions = new ObservableCollection<string>(Enum.GetNames(typeof(Model.Handling)));
            this.HandlingPositionsOptions = new ObservableCollection<string>(Enum.GetNames(typeof(Model.HandlingPosition)));
            this.ProtectionOptions = new ObservableCollection<string>(Enum.GetNames(typeof(Model.Protection)));
        }

        //Pathology
        private void SelectAll()
        {
            foreach (var pathology in PathologiesA)
            {
                pathology.IsSelected = true;
            }
        }

        private void SelectNone()
        {
            foreach (var pathology in PathologiesA)
            {
                pathology.IsSelected = false;
            }

        }

        private void AddPathology()
        {
            this._navigationService.Navigate("PathologiesMultiSelectView");
        }

        private void CreatePathology()
        {
            this._navigationService.Navigate("CreatePathologyView");
        }

        private void GetSelectedPathologies()
        {
            this.Pathologies = PathologiesA.Where(p => p.IsSelected).Select(p => p.Item).ToList();
        }

        private void GetPathologies()
        {
            this.PathologiesA = new ObservableCollection<SelectableItemWrapper<Pathology>>(this._pathologyRepository.GetPathologies().Select(pathology => new SelectableItemWrapper<Pathology>() { Item = pathology }));
        }
        #endregion
    }
}
