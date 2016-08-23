using System;
using System.Collections.Generic;
using ArtApp.Model.Interfaces;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace ArtApp.Model
{
    public class Project : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; }

        [ManyToMany(typeof(WorkProject), CascadeOperations = CascadeOperation.All)]
        public List<Work> Works { get; set; }
    }
}
