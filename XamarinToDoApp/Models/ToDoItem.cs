using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace XamarinToDoApp.Models
{
    public class ToDoItem
    {
        public string Text { get; set; }

        public bool _Completed = false;

        public bool Completed
        {
            get
            {
                return _Completed;
            }
            set
            {
                _Completed = value;
                this.OnPropertyChanged("Completed");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ToDoItem(string text, bool compelte)
        {
            Text = text;
            Completed = compelte;
        }

    }
}
