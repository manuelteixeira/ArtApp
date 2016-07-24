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
        private readonly ConditionReportRepository _conditionReportRepository; 
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
            this._conditionReportRepository = new ConditionReportRepository();

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


            await this._pageDialogService.DisplayAlert("Condition Report",
                        "Condition Report edited: New Title: " + conditionReport.Title, "Ok");
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
