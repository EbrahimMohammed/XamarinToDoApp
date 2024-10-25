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
        private SqlLiteToDoItemDetailsStore _detailsStore;
        public SqlLiteToDoItemStore(ISQLiteDb db)
        {
            _connection = db.GetConnection();
            _connection.CreateTableAsync<ToDoItem>();
            _connection.CreateTableAsync<ToDoItemDetails>(); // Ensure this is created too

        }

     

        public async Task Delete(ToDoItem toDoItem)
        {
            await _connection.DeleteAsync(toDoItem);
        }

        public async Task<IEnumerable<ToDoItem>> GetAll()
        {
            return await _connection.Table<ToDoItem>().ToListAsync();
        }

        public async Task Update(ToDoItem toDoItem)
        {
            await _connection.UpdateAsync(toDoItem);
        }


        public async Task AddToDoItemWithDetails(ToDoItem toDoItem, ToDoItemDetails toDoItemDetails)
        {
            await _connection.InsertAsync(toDoItem);

            toDoItemDetails.ToDoItemId = toDoItem.Id;
            await _connection.InsertAsync(toDoItemDetails);

            toDoItem.ToDoItemDetailsId = toDoItemDetails.ToDoItemDetailsId;
            await _connection.UpdateAsync(toDoItem);
        }

    }
}
