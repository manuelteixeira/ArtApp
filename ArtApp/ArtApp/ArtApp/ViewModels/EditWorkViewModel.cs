using ArtApp.Model;
using ArtApp.Repositories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace ArtApp.ViewModels
{
    public class EditWorkViewModel : BindableBase
    {
        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;
        private readonly WorkRepository _workRepository;

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

        public DelegateCommand EditWorkCommand { get; private set; } 

        public EditWorkViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this._navigationService = navigationService;
            this._pageDialogService = pageDialogService;
            this._workRepository = new WorkRepository();
            
            this.EditWorkCommand = new DelegateCommand(this.EditWork);

        }

        private async void EditWork()
        {
            Work work = new Work()
            {
                Title = this.Title,
                Description = this.Description,
                //The rest of the work attributes 
            };

            await this._pageDialogService.DisplayAlert("Work", "Work edited: New Title: " + work.Title, "Ok");
            this._navigationService.GoBack();

            //IMPLEMENTAR

            //if (await this._workRepository.PutWorkAsync(work.Id, work) != null)
            //{
            //    await this._pageDialogService.DisplayAlert("Work", "Work edited", "Ok");
            //    this._navigationService.GoBack();
            //}
            //else
            //{
            //    await this._pageDialogService.DisplayAlert("Work", "Failed to edit work", "Ok");
            //}
        }
    }
}
