using System.Collections.ObjectModel;
using System.Linq;
using ArtApp.Model;
using ArtApp.Repositories.Database;
using Plugin.Media;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using WorkRepository = ArtApp.Repositories.WorkRepository;

namespace ArtApp.ViewModels
{
    public class EditWorkViewModel : BindableBase, INavigationAware
    {
        #region Services
        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;
        private readonly WorkRepository _workRepository;
        private readonly Repositories.Database.WorkRepository _workDatabase;
        private readonly ClassificationRepository _classificationRepository;
        private readonly ArtTypeRepository _artTypeRepository;
        #endregion

        #region Properties
        //Need to be completed with the rest of the work attributes
        private int Id { get; set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string _photoPath;
        public string PhotoPath
        {
            get { return _photoPath; }
            set { SetProperty(ref _photoPath, value); }
        }

        private ObservableCollection<Author> _authors;
        public ObservableCollection<Author> Authors
        {
            get { return _authors; }
            set { SetProperty(ref _authors, value); }
        }

        private Classification _selectedClassification;
        public Classification SelectedClassification
        {
            get { return _selectedClassification; }
            set { SetProperty(ref _selectedClassification, value); }
        }

        private ObservableCollection<Classification> _classifications;
        public ObservableCollection<Classification> Classifications
        {
            get { return _classifications; }
            set { SetProperty(ref _classifications, value); }
        }

        private ArtType _selectedArtType;
        public ArtType SelectedArtType
        {
            get { return _selectedArtType; }
            set { SetProperty(ref _selectedArtType, value); }
        }

        private ObservableCollection<ArtType> _artTypes;
        public ObservableCollection<ArtType> ArtTypes
        {
            get { return _artTypes; }
            set { SetProperty(ref _artTypes, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand EditWorkCommand { get; private set; }
        public DelegateCommand AddAuthorCommand { get; private set; }
        public DelegateCommand TakePhotoCommand { get; private set; }
        public DelegateCommand PickPhotoCommand { get; private set; }

        #endregion


        public EditWorkViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this._navigationService = navigationService;
            this._pageDialogService = pageDialogService;
            this._workRepository = new WorkRepository();
            this._workDatabase = new Repositories.Database.WorkRepository();
            this._classificationRepository = new ClassificationRepository();
            this._artTypeRepository = new ArtTypeRepository();

            this.EditWorkCommand = new DelegateCommand(this.EditWork);
            this.AddAuthorCommand = new DelegateCommand(this.AddAuthor);
            this.TakePhotoCommand = new DelegateCommand(this.TakePhoto);
            this.PickPhotoCommand = new DelegateCommand(this.PickPhoto);

            GetArtTypes();
            GetClassifications();

        }

        private void GetArtTypes()
        {
            this._artTypes = new ObservableCollection<ArtType>(this._artTypeRepository.GetArtTypes());
        }

        private void GetClassifications()
        {
            this._classifications = new ObservableCollection<Classification>(this._classificationRepository.GetClassifications());
        }


        #region Commands Methods

        private async void EditWork()
        {

            RemoveAuthors();

            Work work = new Work()
            {
                Id = this.Id,
                Title = this.Title,
                Description = this.Description,
                PhotoPath = this.PhotoPath,
                Authors = this.Authors.ToList(),
                ArtType = this.SelectedArtType,
                Classification = this.SelectedClassification,
                //The rest of the work attributes 
            };

            if (this._workDatabase.SaveWork(work) != 0)
            {
                await this._pageDialogService.DisplayAlert("Work", "Work edited: New Title: " + work.Title, "Ok");
                await this._navigationService.GoBack();
            }
            else
            {
                await this._pageDialogService.DisplayAlert("Work", "Work edit failed", "Ok");
                await this._navigationService.GoBack();
            }

        }

        private void RemoveAuthors()
        {
            ObservableCollection<Author> temp = new ObservableCollection<Author>();

            foreach (var author in Authors)
            {
                if (!string.IsNullOrEmpty(author.Name))
                {
                    temp.Add(author);
                }
            }

            if (temp.Count == 0)
            {
                this.Authors = new ObservableCollection<Author>();
                this.Authors.Add(new Author() { Name = "No Authors" });
            }
            else
            {
                this.Authors = temp;
            }
        }

        private void AddAuthor()
        {

            this.Authors.Add(new Author() { Name = "" });
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

            this.PhotoPath = file.Path;

            await this._pageDialogService.DisplayAlert("File Location", file.Path, "OK");
        }

        private async void PickPhoto()
        {
            await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.PickPhotoAsync();

            this.PhotoPath = file.Path;
        }

        #endregion

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("id"))
            {
                Work work = _workDatabase.GetWork((int)parameters["id"]);

                this.Id = work.Id;
                this.Title = work.Title;
                this.Description = work.Description;
                this.PhotoPath = work.PhotoPath;
                this.Authors = new ObservableCollection<Author>(work.Authors);
                this.SelectedClassification = work.Classification;
                this.SelectedArtType = work.ArtType;
            }
        }
    }
}
