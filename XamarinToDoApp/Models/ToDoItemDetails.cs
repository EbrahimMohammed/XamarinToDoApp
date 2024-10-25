using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinToDoApp.Models
{
    public class ToDoItemDetails
    {
        [PrimaryKey, AutoIncrement]
        public int ToDoItemDetailsId { get; set; }

        public string Details { get; set; }

        [Unique]
        public int ToDoItemId { get; set; }

        [Ignore]
        public ToDoItem ToDoItem { get; set; }

    }
}
