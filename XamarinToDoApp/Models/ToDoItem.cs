using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace XamarinToDoApp.Models
{
    public class ToDoItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Text { get; set; }

        public bool Completed { get; set; }

        public int? ToDoItemDetailsId { get; set; }

        public ToDoItem()
        {
           
        }

    }
}
