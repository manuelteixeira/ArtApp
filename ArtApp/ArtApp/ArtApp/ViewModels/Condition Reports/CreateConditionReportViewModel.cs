using ArtApp.Model;
using Prism.Mvvm;
using ArtApp.Repositories;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace ArtApp.ViewModels
{
    public class CreateConditionReportViewModel : BindableBase
    {

        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;
        private readonly ConditionReportRepository _conditionReportRepository;


        #region Properties
        //Need to be completed with the rest of the conditon Report attributes
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand CreateConditionReportCommand { get; private set; } 
        #endregion


        public CreateConditionReportViewModel(IPageDialogService pageDialogService, INavigationService navigationService)
        {
            this._conditionReportRepository = new ConditionReportRepository();
            this._pageDialogService = pageDialogService;
            this._navigationService = navigationService;

            this.CreateConditionReportCommand = new DelegateCommand(CreateConditionReport);

        }

        #region Command methods
        private async void CreateConditionReport()
        {
            ConditionReport conditionReport = new ConditionReport();
            {
                Title = this.Title;
                //The rest of the ConditionReport attributes 
            };

            if (await this._conditionReportRepository.PostConditionReportAsync(conditionReport) != null)
            {
                await this._pageDialogService.DisplayAlert("Condition Report", "New condition report created", "Ok");
                this._navigationService.GoBack();
            }
            else
            {
                await this._pageDialogService.DisplayAlert("Condition Report", "Failed to create condition report", "Ok");
            }
        } 
        #endregion
    }
}
