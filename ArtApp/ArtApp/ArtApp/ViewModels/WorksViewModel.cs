using ArtApp.Models;
using ArtApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ArtApp.ViewModels
{
    public class WorksViewModel : INotifyPropertyChanged
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


        private List<Work> _works;
        public List<Work> Works
        {
            get { return _works; }
            set
            {
                _works = value;
                //OnPropertyChanged();
            }
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
                foundWorks = _works;
            }
            else
            {
                foundWorks = _works.FindAll(p => p.Title.ToLower().Contains(this.SearchText.ToLower()));
            }

            foreach (var foundWork in foundWorks)
            {
                this.WorksSearch.Add(foundWork);
            }
        }

        #endregion


        public WorksViewModel()
        {
            this.SearchCommand = new Command(this.ExecuteSearchCommand, this.CanExecuteSearchCommand);

            var worksServices = new WorksServices();

            Works = worksServices.GetWorks();

            _worksSearch = new ObservableCollection<Work>(_works);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
