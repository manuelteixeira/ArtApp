using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using ArtApp.Model;

namespace ArtApp.ViewModels
{
    public class ConditionReportViewModel : BindableBase
    {

        public int ConditionReportId { get; set; }
        public string Title { get; set; }
        public float RH { get; set; }
        public float Lux { get; set; }
        public float Temperature { get; set; }

        public Handling? Handling { get; set; }
        public HandlingPosition? HandlingPosition { get; set; }
        public Protection? FrontProtection { get; set; }
        public Protection? BackProtection { get; set; }

        public DateTime Date { get; set; }
        public string MadeBy { get; set; }
        public string Notes { get; set; }

        public virtual Work Work { get; set; }

        public ConditionReportViewModel()
        {

        }
    }
}
