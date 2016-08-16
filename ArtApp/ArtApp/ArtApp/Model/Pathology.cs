using System.Collections.Generic;
using ArtApp.Model.Interfaces;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace ArtApp.Model
{
    public class Pathology : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

        [ManyToMany(typeof(ConditionReportPathology), CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public List<ConditionReport> ConditionReports { get; set; }
    }
}
