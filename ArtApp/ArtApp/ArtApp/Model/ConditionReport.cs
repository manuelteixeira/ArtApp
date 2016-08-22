using System;
using System.Collections.Generic;
using ArtApp.Model.Interfaces;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

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

    public class ConditionReport : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

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

        //public virtual Work Work { get; set; }

        [ManyToMany(typeof(ConditionReportPhoto), CascadeOperations = CascadeOperation.All)]
        public List<Photo> Photos { get; set; }

        [ManyToMany(typeof(ConditionReportPathology), CascadeOperations = CascadeOperation.All)]
        public List<Pathology> Pathologies { get; set; }

        [ForeignKey(typeof(Work))]
        public int WorkId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Work Work { get; set; }
    }
}
