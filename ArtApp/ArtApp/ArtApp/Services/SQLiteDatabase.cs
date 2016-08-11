using System.Collections.Generic;
using System.Linq;
using ArtApp.Model;
using ArtApp.Model.Interfaces;
using ArtApp.Services.Interfaces;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using Xamarin.Forms;

namespace ArtApp.Services
{
    public class SQLiteDatabase
    {

        static readonly object Locker = new object();

        SQLiteConnection database;

        public SQLiteDatabase()
        {
            this.database = DependencyService.Get<ISQLite>().GetConnection();

            //Clean Database
            //database.DropTable<Work>();
            //database.DropTable<Author>();
            //database.DropTable<WorkAuthor>();

            //All sql tables: Add as we create
            database.CreateTable<TodoItem>();
            database.CreateTable<Author>();
            database.CreateTable<Work>();
            database.CreateTable<WorkAuthor>();
            database.CreateTable<TodoItem>();
        }

        //Get all items in the database method
        public IEnumerable<T> GetItems<T>() where T : class, IEntity, new()
        {
            lock (Locker)
            {
                return (from i in database.Table<T>() select i).ToList();
            }
        }

        //Get all with children
        public IEnumerable<T> GetItemsWithChildren<T>() where T : class, IEntity, new()
        {
            lock (Locker)
            {
                return database.GetAllWithChildren<T>();
            }
        }

        //Get specific item in the database method with Id
        public T GetItem<T>(int id) where T : class, IEntity, new()
        {
            lock (Locker)
            {
                return database.Table<T>().FirstOrDefault(x => x.Id == id);
            }
        }

        //Get with children
        public T GetItemWithChildren<T>(int id) where T : class, IEntity, new()
        {
            lock (Locker)
            {
                return database.GetWithChildren<T>(id);
            }
        }

        public int SaveItem<T>(T item) where T : IEntity
        {
            lock (Locker)
            {
                if (item.Id != 0)
                {
                    database.Update(item);
                    return item.Id;
                }
                return database.Insert(item);
            }
        }

        public int SaveWithChildren<T>(T item) where T : IEntity
        {
            lock (Locker)
            {
                database.InsertOrReplaceWithChildren(item);
                return item.Id;

                //if (item.Id != 0)
                //{
                //    database.UpdateWithChildren(item);
                //    return item.Id;
                //}
                //else
                //{
                //    database.InsertOrReplaceWithChildren(item);
                //    return item.Id;
                //}
            }
        }

        public int DeleteItem<T>(int id) where T : IEntity, new()
        {
            lock (Locker)
            {
                return database.Delete<T>(id);
            }
        }

        public List<T> Query<T>(string query, params object[] args) where T : class, IEntity
        {
            lock (Locker)
            {
                return database.Query<T>(query, args);
            }
        }
    }
}
