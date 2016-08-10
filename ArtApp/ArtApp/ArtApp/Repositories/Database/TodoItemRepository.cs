using System.Collections.Generic;
using ArtApp.Model;
using ArtApp.Services;

namespace ArtApp.Repositories.Database
{
    public class TodoItemRepository
    {

        private readonly SQLiteDatabase _database;

        public TodoItemRepository()
        {
            _database = new SQLiteDatabase();
        }

        public IEnumerable<TodoItem> GetTodoItems()
        {
            return _database.GetItems<TodoItem>();
        }

        public TodoItem GetTodoItem(int id)
        {
            return _database.GetItem<TodoItem>(id);
        }

        public int SaveTodoItem(TodoItem todoItem)
        {
            return _database.SaveItem(todoItem);
        }

        public int DeleteTodoItem(int id)
        {
            return _database.DeleteItem<TodoItem>(id);
        }

        public IEnumerable<TodoItem> GetItemsNotDone()
        {

            return _database.Query<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");

        }
    }
}
