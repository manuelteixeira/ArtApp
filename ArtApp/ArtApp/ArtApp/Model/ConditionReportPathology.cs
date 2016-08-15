using ArtApp.Model.Interfaces;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace ArtApp.Model
{
    public class ConditionReportPathology : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(ConditionReport))]
        public int ConditionReportId { get; set; }

        [ForeignKey(typeof(Pathology))]
        public int PathologyId { get; set; }
    }
}
