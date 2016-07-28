using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArtApp.Model
{
    public enum Handling
    {
        Cotton_gloves, Latex_gloves
    }

    public enum HandlingPosition
    {
        Vertical, Horizontal
    }

    public enum Protection
    {
        Glass, Plexiglass, Climabox, Tablex, Policarbonade, None, Others
    }

    //Confirmar atributos que faltam
    public class ConditionReport
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

    }
}
