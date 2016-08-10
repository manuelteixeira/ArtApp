using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ArtApp.Model;
using ArtApp.Services.Interfaces;
using SQLite.Net;
using Xamarin.Forms;

namespace ArtApp.Database
{
    public class TodoDatabase
    {
        static object locker = new object();

        SQLiteConnection database;

        public TodoDatabase()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();

            var a = database.CreateTable<TodoItem>();

            a.ToString();

        }

        public IEnumerable<TodoItem> GetItems()
        {
            lock (locker)
            {
                return (from i in database.Table<TodoItem>() select i).ToList();
            }
        }

        public IEnumerable<TodoItem> GetItemsNotDone()
        {
            lock (locker)
            {
                return database.Query<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
            }
        }

        public TodoItem GetItem(int id)
        {
            lock (locker)
            {
                return database.Table<TodoItem>().FirstOrDefault(x => x.Id == id);
            }
        }

        public int DeleteItem(int id)
        {
            lock (locker)
            {
                return database.Delete<TodoItem>(id);
            }
        }

        public int SaveTodoItem(TodoItem todoItem)
        {
            lock (locker)
            {
                if (todoItem.Id != 0)
                {
                    database.Update(todoItem);
                    return todoItem.Id;
                }
                else
                {
                    return database.Insert(todoItem);
                }
            }
        }
    }
}
