using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ArtApp.Model;
using ArtApp.Services.Interfaces;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using Xamarin.Forms;

namespace ArtApp.Database
{
    public class WorkDatabase
    {
        static object locker = new object();

        SQLiteConnection database;

        public WorkDatabase()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();

            database.DropTable<Work>();
            database.DropTable<Author>();
            database.DropTable<WorkAuthor>();

            database.CreateTable<Work>();
            database.CreateTable<Author>();
            database.CreateTable<WorkAuthor>();
        }

        public IEnumerable<Work> GetWorks()
        {
            lock (locker)
            {
                return (from i in database.Table<Work>() select i).ToList();
            }
        }

        public Work GetWork(int id)
        {
            lock (locker)
            {
                var a = database.Table<Author>().First();
                return database.GetWithChildren<Work>(id);
                //return database.Table<Work>().FirstOrDefault(x => x.ID == id);
            }
        }

        public int DeleteWork(int id)
        {
            lock (locker)
            {
                return database.Delete<Work>(id);
            }
        }

        public int SaveWork(Work work)
        {
            lock (locker)
            {
                if (work.ID != 0)
                {
                    //database.Update(work);
                    database.UpdateWithChildren(work);
                    return work.ID;
                }
                else
                {
                    return database.Insert(work);
                }
            }
        }

        public void Insert(object obj)
        {
            database.InsertOrReplaceWithChildren(obj);
        }
    }
}
