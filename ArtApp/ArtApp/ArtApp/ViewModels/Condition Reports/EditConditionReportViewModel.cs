using ArtApp.Model;
using ArtApp.Repositories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace ArtApp.ViewModels
{
    public class EditConditionReportViewModel : BindableBase
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

        private string _title;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
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
        }


        #region Command Methods

        private async void EditConditionReport()
        {
            ConditionReport conditionReport = new ConditionReport()
            {
                Title = this.Title
                //The rest of the Condition Report attributes 
            };

            if (await this._conditionReportMockRepository.PutConditionReportAsync(
                        conditionReport.ConditionReportId.ToString(), conditionReport) != null)
            {
                await this._pageDialogService.DisplayAlert("Condition Report",
                    "Condition Report edited: New Title: " + conditionReport.Title, "Ok");
            }
            else
            {
                await
                    this._pageDialogService.DisplayAlert("Condition Report", "Failed to edit the condition report", "Ok");
            }
            this._navigationService.GoBack();

            ////IMPLEMENTAR
            //if (await this._conditionReportRepository.PutConditionReportAsync(conditionReport.ConditionReportId.ToString(), conditionReport) != null)
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
    }
}
