using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinToDoApp.Models
{
    public class ToDoItem
    {
        public string Text { get; set; }

        public bool Complete { get; set; }

        public ToDoItem(string text, bool compelte)
        {
            Text = text;
            Complete = compelte;
        }

    }
}
