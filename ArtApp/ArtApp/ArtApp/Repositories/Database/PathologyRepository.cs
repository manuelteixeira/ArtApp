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
                });
                _database.SaveItem(new Pathology()
                {
                    Name = "Water",
                    Description = "Pathology caused by Water",
                });
                _database.SaveItem(new Pathology()
                {
                    Name = "Condensation",
                    Description = "Pathology caused by Water",
                });
                _database.SaveItem(new Pathology()
                {
                    Name = "Deteoration",
                    Description = "Pathology caused by Deteoration",
                });
                _database.SaveItem(new Pathology()
                {
                    Name = "Finger Marks",
                    Description = "Pathology caused by Finger Marks",
                });
                _database.SaveItem(new Pathology()
                {
                    Name = "Hinge Failure",
                    Description = "Pathology caused by Hinge Failure",
                });
                _database.SaveItem(new Pathology()
                {
                    Name = "Mold",
                    Description = "Pathology caused by Mold",
                });
                _database.SaveItem(new Pathology()
                {
                    Name = "Residue",
                    Description = "Pathology caused by Residue",
                });
                _database.SaveItem(new Pathology()
                {
                    Name = "Split",
                    Description = "Pathology caused by Split",
                });
                _database.SaveItem(new Pathology()
                {
                    Name = "Warping",
                    Description = "Pathology caused by Warping",
                });
                _database.SaveItem(new Pathology()
                {
                    Name = "Discoloration",
                    Description = "Pathology caused by Discoloration",
                });
                _database.SaveItem(new Pathology()
                {
                    Name = "Insect",
                    Description = "Pathology caused by Insects",
                });
                _database.SaveItem(new Pathology()
                {
                    Name = "Stain",
                    Description = "Pathology caused by Stain",
                });
                _database.SaveItem(new Pathology()
                {
                    Name = "Bubble",
                    Description = "Pathology caused by Bubble",
                });
                _database.SaveItem(new Pathology()
                {
                    Name = "Dent",
                    Description = "Pathology caused by Dent",
                });
                _database.SaveItem(new Pathology()
                {
                    Name = "Smudge",
                    Description = "Pathology caused by Smudge",
                });
                _database.SaveItem(new Pathology()
                {
                    Name = "Acid",
                    Description = "Pathology caused by Acid",
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
