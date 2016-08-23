using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ArtApp.Controls;
using ArtApp.Model;
using ArtApp.Repositories;
using ArtApp.Repositories.Database;
using Plugin.Media;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ConditionReportRepository = ArtApp.Repositories.Database.ConditionReportRepository;

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
        private readonly ConditionReportRepository _conditionReportRepository;
        private readonly PathologyRepository _pathologyRepository;
        private readonly Repositories.Database.WorkRepository _workRepository;
        #endregion

        #region Properties
        //Need to be completed with the rest of the condition Report attributes
        private int Id { get; set; }

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

        #endregion

        #region Commands

        public DelegateCommand EditConditionReportCommand { get; private set; }
        public DelegateCommand TakePhotoCommand { get; private set; }
        public DelegateCommand PickPhotoCommand { get; private set; }
        public DelegateCommand SelectAllCommand { get; private set; }
        public DelegateCommand SelectNoneCommand { get; private set; }
        #endregion


        public EditConditionReportViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this._navigationService = navigationService;
            this._pageDialogService = pageDialogService;
            //For API objects
            //this._conditionReportRepository = new ConditionReportRepository();
            this._conditionReportMockRepository = new ConditionReportMockRepository();
            this._conditionReportRepository = new ConditionReportRepository();
            this._pathologyRepository = new PathologyRepository();
            this._workRepository = new Repositories.Database.WorkRepository();

            this.EditConditionReportCommand = new DelegateCommand(this.EditConditionReport);
            this.TakePhotoCommand = new DelegateCommand(TakePhoto);
            this.PickPhotoCommand = new DelegateCommand(PickPhoto);
            this.SelectAllCommand = new DelegateCommand(SelectAll);
            this.SelectNoneCommand = new DelegateCommand(SelectNone);

            //Populate Pickers
            PopulatePickers();
        }

        #region Command Methods

        private async void EditConditionReport()
        {
            GetSelectedPathologies();

            ConditionReport conditionReport = new ConditionReport()
            {
                Id = this.Id,
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
                Work = this.WorkSelected,
                Pathologies = this.Pathologies
            };

            if (this._conditionReportRepository.SaveConditionReport(conditionReport) != 0)
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
        }

        private void GetWorks()
        {
            this.Works = new ObservableCollection<Work>(this._workRepository.GetWorks());
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

        private void GetSelectedPathologies()
        {
            this.Pathologies = PathologiesA.Where(p => p.IsSelected).Select(p => p.Item).ToList();
        }

        private void GetPathologies()
        {
            this.PathologiesA = new ObservableCollection<SelectableItemWrapper<Pathology>>(this._pathologyRepository.GetPathologies().Select(pathology => new SelectableItemWrapper<Pathology>() { Item = pathology }));
            this.ShowActualPathologies();
        }

        private void ShowActualPathologies()
        {

            foreach (var pathology in PathologiesA)
            {
                if (Pathologies.Exists(p => p.Name == pathology.Item.Name))
                {
                    pathology.IsSelected = true;
                }
            }
        }

        private void PopulatePickers()
        {
            this.HandlingOptions = new ObservableCollection<string>(Enum.GetNames(typeof(Model.Handling)));
            this.HandlingPositionsOptions = new ObservableCollection<string>(Enum.GetNames(typeof(Model.HandlingPosition)));
            this.ProtectionOptions = new ObservableCollection<string>(Enum.GetNames(typeof(Model.Protection)));
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
                ConditionReport conditionReport = this._conditionReportRepository.GetConditionReport((int)parameters["id"]);

                this.Id = conditionReport.Id;
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
                this.WorkSelected = conditionReport.Work;
                this.Pathologies = conditionReport.Pathologies;
            }

            //Pathologies
            GetPathologies();

            //Works
            GetWorks();
        }
    }
}
