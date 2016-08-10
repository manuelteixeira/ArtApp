using System;
using System.Collections.Generic;
using ArtApp.Model.Interfaces;
using SQLite.Net.Attributes;

namespace ArtApp.Model
{
    public class Project : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; }
        public ICollection<Work> Works { get; set; }
    }
}
