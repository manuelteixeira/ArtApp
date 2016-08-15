using System.Collections.Generic;
using ArtApp.Model;
using ArtApp.Services;

namespace ArtApp.Repositories.Database
{
    public class ConditionReportRepository
    {

        private readonly SQLiteDatabase _database;

        public ConditionReportRepository()
        {
            _database = new SQLiteDatabase();
        }

        public IEnumerable<ConditionReport> GetConditionReports()
        {
            return _database.GetItemsWithChildren<ConditionReport>();
        }

        public ConditionReport GetConditionReport(int id)
        {
            return _database.GetItemWithChildren<ConditionReport>(id);
        }

        public int SaveConditionReport(ConditionReport conditionReport)
        {
            return _database.SaveWithChildren(conditionReport);
        }

        public int DeleteConditionReport(int id)
        {
            return _database.DeleteItem<ConditionReport>(id);
        }
    }
}
