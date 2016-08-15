using System.Collections.Generic;
using ArtApp.Model;
using ArtApp.Services;

namespace ArtApp.Repositories.Database
{
    public class PathologyRepository
    {

        private readonly SQLiteDatabase _database;

        public PathologyRepository()
        {
            _database = new SQLiteDatabase();
            CreatePathologies();
        }

        private void CreatePathologies()
        {
            int count = _database.CountElements<Pathology>();
            if (count == 0)
            {
                _database.SaveItem(new Pathology()
                {
                    Name = "Abrasion",
                    Description = "Pathology caused by abrasion",
                    Grade = 12,
                });
                _database.SaveItem(new Pathology()
                {
                    Name = "Water",
                    Description = "Pathology caused by Water",
                    Grade = 20,
                });
            }
        }

        public IEnumerable<Pathology> GetPathologies()
        {
            return _database.GetItemsWithChildren<Pathology>();
        }

        public Pathology GetPathology(int id)
        {
            return _database.GetItemWithChildren<Pathology>(id);
        }

        public int SavePathology(Pathology pathology)
        {
            return _database.SaveWithChildren(pathology);
        }

        public int DeletePathology(int id)
        {
            return _database.DeleteItem<Pathology>(id);
        }

    }
}
