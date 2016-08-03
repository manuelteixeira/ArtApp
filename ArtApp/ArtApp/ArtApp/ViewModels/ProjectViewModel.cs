using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using ArtApp.Model;

namespace ArtApp.ViewModels
{
    public class ProjectViewModel : BindableBase
    {
        public int ProjectId { get; set; }

        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; }
        public ICollection<Work> Works { get; set; }

        public ProjectViewModel()
        {

        }
    }
}
