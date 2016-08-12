using System.Collections.ObjectModel;
using System.Linq;
using Prism.Commands;
using Prism.Mvvm;
using ArtApp.Model;
using ArtApp.Repositories;
using Plugin.Media;
using Prism.Navigation;
using Prism.Services;

namespace ArtApp.ViewModels
{
    public class CreateWorkViewModel : BindableBase
    {
        #region Services
        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;
        private readonly WorkRepository _workRepository;
        private readonly Repositories.Database.WorkRepository _workDatabase;
        #endregion

        #region Properties
        private int Id { get; set; }

        //Need to be completed with the rest of the work attributes
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
        #endregion

        #region Commands
        public DelegateCommand CreateWorkCommand { get; private set; }

        public DelegateCommand AddAuthorCommand { get; private set; }

        public DelegateCommand TakePhotoCommand { get; private set; }

        public DelegateCommand PickPhotoCommand { get; private set; } 
        #endregion


        public CreateWorkViewModel(IPageDialogService pageDialogService, INavigationService navigationService)
        {
            this._workRepository = new WorkRepository();
            this._pageDialogService = pageDialogService;
            this._navigationService = navigationService;
            this._workDatabase = new Repositories.Database.WorkRepository();

            this.CreateWorkCommand = new DelegateCommand(CreateWork);
            this.AddAuthorCommand = new DelegateCommand(this.AddAuthor);
            this.TakePhotoCommand = new DelegateCommand(this.TakePhoto);
            this.PickPhotoCommand = new DelegateCommand(this.PickPhoto);

            this.Authors = new ObservableCollection<Author>();
        }



        #region Command methods
        //private async void CreateWork()
        //{
        //    Work work = new Work()
        //    {
        //        Title = this.Title,
        //        Description = this.Description,
        //        //The rest of the work attributes 
        //    };

        //    if (await this._workRepository.PostWorkAsync(work) != null)
        //    {
        //        await this._pageDialogService.DisplayAlert("Work", "New work created", "Ok");
        //        this._navigationService.GoBack();
        //    }
        //    else
        //    {
        //        await this._pageDialogService.DisplayAlert("Work", "Failed to create work", "Ok");
        //    }
        //}

        private async void CreateWork()
        {
            RemoveAuthors();

            Work work = new Work()
            {
                Id = this.Id,
                Title = this.Title,
                Description = this.Description,
                Authors = this.Authors.ToList(),
                PhotoPath = this.PhotoPath
                //The rest of the work attributes 
            };


            if (_workDatabase.SaveWork(work) != 0)
            {
                await this._pageDialogService.DisplayAlert("Work", "The work was created successfully", "ok");
                await this._navigationService.GoBack();
            }
            else
            {
                await this._pageDialogService.DisplayAlert("Work", "The work failed to create", "ok");
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
                this.Authors.Add(new Author() {Name = "No Authors"});
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
    }
}
