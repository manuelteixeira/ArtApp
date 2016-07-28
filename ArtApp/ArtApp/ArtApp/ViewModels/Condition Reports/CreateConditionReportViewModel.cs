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

        //For API objects
        //private readonly ConditionReportRepository _conditionReportRepository;
        //For Mock Objects
        private readonly ConditionReportMockRepository _conditionReportMockRepository;


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
            //For API objects
            //this._conditionReportRepository = new ConditionReportRepository();
            //For mock objects
            this._conditionReportMockRepository = new ConditionReportMockRepository();

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

            //For API objects
            //if (await this._conditionReportRepository.PostConditionReportAsync(conditionReport) != null)
            if(await this._conditionReportMockRepository.PostConditionReportAsync(conditionReport) != null)
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
