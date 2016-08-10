using System.Collections.Generic;
using ArtApp.Model;
using ArtApp.Services;

namespace ArtApp.Repositories.Database
{
    public class WorkRepository
    {

        private readonly SQLiteDatabase _database;

        public WorkRepository()
        {
            _database = new SQLiteDatabase();
        }

        public IEnumerable<Work> GetWorks()
        {
            return _database.GetItemsWithChildren<Work>();
        }

        public Work GetWork(int id)
        {
            return _database.GetItemWithChildren<Work>(id);
        }

        public int SaveWork(Work work)
        {
            return _database.SaveWithChildren(work);
        }

        public int DeleteWork(int id)
        {
            return _database.DeleteItem<Work>(id);
        }

    }
}
