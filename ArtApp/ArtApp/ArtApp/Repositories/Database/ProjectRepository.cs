using System.Collections.Generic;
using ArtApp.Model;
using ArtApp.Services;

namespace ArtApp.Repositories.Database
{
    public class ProjectRepository
    {
        private readonly SQLiteDatabase _database;

        public ProjectRepository()
        {
            _database = new SQLiteDatabase();
        }

        public IEnumerable<Project> GetProjects()
        {
            return _database.GetItemsWithChildren<Project>();
        }

        public Project GetProject(int id)
        {
            return _database.GetItemWithChildren<Project>(id);
        }

        public int SaveProject(Project project)
        {
            return _database.SaveWithChildren(project);
        }

        public int DeleteProject(int id)
        {
            return _database.DeleteItem<Project>(id);
        }
    }
}
