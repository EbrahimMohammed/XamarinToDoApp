using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinToDoApp.Models;

namespace XamarinToDoApp.Persistance
{
    public interface IToDoItemDetailsStore
    {
         Task<ToDoItemDetails> GetById(int id);
         Task Add(ToDoItemDetails toDoItemDetails);
         Task Update(ToDoItemDetails toDoItemDetails);
         Task Delete(ToDoItemDetails toDoItemDetails);

         

    }
}
