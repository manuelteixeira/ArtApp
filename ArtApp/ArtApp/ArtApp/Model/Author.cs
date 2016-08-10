using System.Collections.Generic;
using ArtApp.Model.Interfaces;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace ArtApp.Model
{
    public class Author : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        [ManyToMany(typeof(WorkAuthor), CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public List<Work> Works { get; set; }
    }
}
