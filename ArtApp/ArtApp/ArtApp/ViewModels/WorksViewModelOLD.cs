using ArtApp.Models;
using ArtApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace ArtApp.ViewModels
{
    public class WorksViewModelOLD : INotifyPropertyChanged
    {

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                }
            }
        }

        private readonly ObservableCollection<Work> _worksSearch;
        public ObservableCollection<Work> WorksSearch => _worksSearch;


        public List<Work> Works { get; set; }

        private Work _selectedWork;
        private string _statusMessage;

        public Work SelectedWork
        {
            get { return _selectedWork; }
            set
            {
                _selectedWork = value;
                OnPropertyChanged();
            }
        }

        public string StatusMessage
        {
            get { return _statusMessage; }
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }


        public WorksViewModelOLD()
        {
            this.SearchCommand = new Command(this.ExecuteSearchCommand, this.CanExecuteSearchCommand);
            this.EditCommand = new Command(this.ExecuteEditCommand, this.CanExecuteEditCommand);
            this.DeleteCommand = new Command(this.ExecuteDeleteCommand, this.CanExecuteDeleteCommand);


            //Singleton?
            var worksServices = new WorksAPIServices();

            Works = worksServices.GetWorks();

            _worksSearch = new ObservableCollection<Work>(Works);
        }


        #region Search Commands and methods
        public ICommand SearchCommand { get; private set; }

        protected virtual bool CanExecuteSearchCommand()
        {
            return true;
        }

        protected virtual void ExecuteSearchCommand()
        {
            this.WorksSearch.Clear();

            IEnumerable<Work> foundWorks;
            if (string.IsNullOrEmpty(this.SearchText))
            {
                foundWorks = Works;
            }
            else
            {
                foundWorks = Works.FindAll(p => p.Title.ToLower().Contains(this.SearchText.ToLower()));
            }

            foreach (var foundWork in foundWorks)
            {
                this.WorksSearch.Add(foundWork);
            }
        }

        #endregion

        #region Edit Commands and methods
        public ICommand EditCommand { get; private set; }

        protected virtual bool CanExecuteEditCommand()
        {
            return true;
        }

        protected virtual void ExecuteEditCommand()
        {
            var worksServices = new WorksAPIServices();

            //Change to the request code from the API? if status code == 200
            if (worksServices.PutWork(_selectedWork.WorkId, _selectedWork))
            {
                StatusMessage = "SUCCESS EDIT!";
            }


        }

        #endregion

        #region Delete Commands and methods
        public ICommand DeleteCommand { get; private set; }

        protected virtual bool CanExecuteDeleteCommand()
        {
            return true;
        }

        protected virtual void ExecuteDeleteCommand()
        {
            var worksServices = new WorksAPIServices();

            //Change to the request code from the API? if status code == 200
            if (worksServices.DeleteWork(_selectedWork.WorkId))
            {
                StatusMessage = "SUCCESS DELETE!";
            }


        }

        #endregion


        #region Property Changed
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
