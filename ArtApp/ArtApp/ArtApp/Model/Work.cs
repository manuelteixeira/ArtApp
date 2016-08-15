using System;
using System.Collections.Generic;
using ArtApp.Model.Interfaces;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace ArtApp.Model
{
    //public enum Classification
    //{
    //    Extremelly_Fragile,
    //    Fragile,
    //    Regular,
    //    Good,
    //    Extremelly_Good,
    //};

    public class Work : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhotoPath { get; set; }
        public DateTime Date { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Heigth { get; set; }

        [ManyToMany(typeof(WorkAuthor), CascadeOperations = CascadeOperation.All)]
        public List<Author> Authors { get; set; }

        [ForeignKey(typeof(Classification))]
        public int ClassificationId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Classification Classification { get; set; }

        [ForeignKey(typeof(ArtType))]
        public int ArtTypeId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public ArtType ArtType { get; set; }

    }
}
