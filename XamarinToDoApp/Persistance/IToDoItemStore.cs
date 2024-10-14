using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using XamarinToDoApp.Models;

namespace XamarinToDoApp.Persistance
{
    public interface IToDoItemStore
    {
        Task<IEnumerable<ToDoItem>> GetAll();
        
        Task Add(ToDoItem contact);
        Task Delete(ToDoItem contact);
        Task Update(ToDoItem contact);
    }
}
