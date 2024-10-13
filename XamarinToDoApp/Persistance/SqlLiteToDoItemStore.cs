using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using XamarinToDoApp.Models;

namespace XamarinToDoApp.Persistance
{
    public class SqlLiteToDoItemStore : IToDoItemStore
    {
        private SQLiteAsyncConnection _connection;

        public SqlLiteToDoItemStore(ISQLiteDb db)
        {
            _connection = db.GetConnection();
            _connection.CreateTableAsync<ToDoItem>();

        }

        public async Task Add(ToDoItem toDoItem)
        {
            await _connection.InsertAsync(toDoItem);
        }

        public async Task Delete(ToDoItem toDoItem)
        {
            await _connection.DeleteAsync(toDoItem);
        }

        public async Task<IEnumerable<ToDoItem>> GetAll()
        {
            return await _connection.Table<ToDoItem>().ToListAsync();
        }
    }
}
