using System.Collections.Generic;
using ArtApp.Model.Interfaces;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace ArtApp.Model
{
    public class Photo : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string PhotoPath { get; set; }

        [ManyToMany(typeof(ConditionReportPhoto), CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public List<ConditionReport> ConditionReports { get; set; }

    }
}
