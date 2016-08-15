using System;
using System.Collections.Generic;
using ArtApp.Model.Interfaces;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace ArtApp.Model
{
    //public enum Handling
    //{
    //    Cotton_gloves, Latex_gloves
    //}

    //public enum HandlingPosition
    //{
    //    Vertical, Horizontal
    //}

    //public enum Protection
    //{
    //    Glass, Plexiglass, Climabox, Tablex, Policarbonade, None, Others
    //}

    //Confirmar atributos que faltam
    public class ConditionReportOLD : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; }
        public float RH { get; set; }
        public float Lux { get; set; }
        public float Temperature { get; set; }

        //public Handling? Handling { get; set; }
        //public HandlingPosition? HandlingPosition { get; set; }
        //public Protection? FrontProtection { get; set; }
        //public Protection? BackProtection { get; set; }

        public DateTime Date { get; set; }
        public string MadeBy { get; set; }
        public string Notes { get; set; }

        //public virtual Work Work { get; set; }

        public List<string> PhotosPath { get; set; }

        [ManyToMany(typeof(ConditionReportPathology), CascadeOperations = CascadeOperation.All)]
        public List<Pathology> Pathologies { get; set; }

    }
}
