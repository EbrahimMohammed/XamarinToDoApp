using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinToDoApp.Models;

namespace XamarinToDoApp.Persistance
{
   public class SqlLiteToDoItemDetailsStore : IToDoItemDetailsStore
    {
        private readonly SQLiteAsyncConnection _connection;

        public SqlLiteToDoItemDetailsStore(ISQLiteDb db)
        {
            _connection = db.GetConnection();
            _connection.CreateTableAsync<ToDoItemDetails>();

        }

        public async Task Add(ToDoItemDetails toDoItemDetails)
        {
            await _connection.InsertAsync(toDoItemDetails);
        }

        public async Task Delete(ToDoItemDetails toDoItemDetails)
        {
            await _connection.DeleteAsync(toDoItemDetails);
        }

        public async Task Update(ToDoItemDetails toDoItemDetails)
        {
            await _connection.UpdateAsync(toDoItemDetails);
        } 
        
        public async Task<ToDoItemDetails> GetById(int id)
        {
            var details =  await _connection.FindAsync<ToDoItemDetails>(id);

            if (details != null)
            {
                details.ToDoItem = await _connection.FindAsync<ToDoItem>(details.ToDoItemId);
            }

            return details;

        } 
        
             

    }
}
