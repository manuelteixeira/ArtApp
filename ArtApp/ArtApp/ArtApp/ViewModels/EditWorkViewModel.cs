using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ArtApp.Models;
using ArtApp.Repositories;
using Xamarin.Forms;

namespace ArtApp.ViewModels
{
    public class EditWorkViewModel : BaseViewModel
    {
        protected readonly Repositories.WorkRepository _workRepository;


        public string WorkId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Technique { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Heigth { get; set; }
        public Classification Classification { get; set; }
        public List<Author> Authors { get; set; }

        //Commands
        public ICommand EditWorkCommand { get; set; }

        public EditWorkViewModel()
        {
            this._workRepository = new WorkRepository();
            this.EditWorkCommand = new Command(EditWork);

        }

        private void EditWork()
        {
            //Converter para object work?
            Work work = new Work()
            {
                Title = this.Title,
                Description = this.Description
            };
            //Conectar com API
            //this._workRepository.PutWorkAsync(this.WorkId, work);
            

            this._messageService.ShowASync("Edit Success");
        }
    }
}
