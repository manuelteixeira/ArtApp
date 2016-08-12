using System.Collections.Generic;
using System.Linq;
using ArtApp.Model;
using ArtApp.Services;

namespace ArtApp.Repositories.Database
{
    public class ClassificationRepository
    {

        private readonly SQLiteDatabase _database;

        public ClassificationRepository()
        {
            _database = new SQLiteDatabase();
            CreateClassifications();

        }

        private void CreateClassifications()
        {
            int count = _database.CountElements<Classification>();
            if (count == 0)
            {
                _database.SaveItem(new Classification()
                {
                    Name = "Extremelly Fragile"
                });
                _database.SaveItem(new Classification()
                {
                    Name = "Fragile"
                });
                _database.SaveItem(new Classification()
                {
                    Name = "Regular"
                });
                _database.SaveItem(new Classification()
                {
                    Name = "Good"
                });
                _database.SaveItem(new Classification()
                {
                    Name = "Very Good"
                });
            }
        }

        public IEnumerable<Classification> GetClassifications()
        {
            return _database.GetItemsWithChildren<Classification>();
        }

        public Classification GetClassification(int id)
        {
            return _database.GetItemWithChildren<Classification>(id);
        }

        public int SaveClassification(Classification classification)
        {
            return _database.SaveWithChildren(classification);
        }

        public int DeleteClassification(int id)
        {
            return _database.DeleteItem<Classification>(id);
        }
    }
}
