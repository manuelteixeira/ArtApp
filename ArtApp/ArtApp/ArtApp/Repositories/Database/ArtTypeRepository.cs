using System.Collections.Generic;
using ArtApp.Model;
using ArtApp.Services;

namespace ArtApp.Repositories.Database
{
    public class ArtTypeRepository
    {
        private readonly SQLiteDatabase _database;

        public ArtTypeRepository()
        {
            _database = new SQLiteDatabase();
            CreateArtTypes();
        }

        private void CreateArtTypes()
        {
            int count = _database.CountElements<ArtType>();
            if (count == 0)
            {
                _database.SaveItem(new ArtType()
                {
                    Name = "Painting"
                });
                _database.SaveItem(new ArtType()
                {
                    Name = "Sculpture"
                });
                _database.SaveItem(new ArtType()
                {
                    Name = "Textile"
                });
                _database.SaveItem(new ArtType()
                {
                    Name = "Photograph"
                });
            }
        }

        public IEnumerable<ArtType> GetArtTypes()
        {
            return _database.GetItemsWithChildren<ArtType>();
        }

        public ArtType GetArtType(int id)
        {
            return _database.GetItemWithChildren<ArtType>(id);
        }

        public int SaveArtType(ArtType artType)
        {
            return _database.SaveWithChildren(artType);
        }

        public int DeleteArtType(int id)
        {
            return _database.DeleteItem<ArtType>(id);
        }
    }
}
