using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinToDoApp.Models;

namespace XamarinToDoApp.ViewModels
{
    public class ToDoListViewModel
    {


        public ObservableCollection<ToDoItem> ToDoItems { get; set; }

        public ToDoListViewModel()
        {
            ToDoItems = new ObservableCollection<ToDoItem>();
        }

        public string NewToDoTextValue { get; set; }

        public ICommand AddNewToDoCommand => new Command(AddNewToDoItem);
        void AddNewToDoItem()
        {
            ToDoItems.Add(new ToDoItem(NewToDoTextValue, false));

        }


        public ICommand RemoveToDoCommand => new Command(RemoveToDoItem);
        void RemoveToDoItem(object o)
        {
            ToDoItem itemToRemove = o as ToDoItem;
            ToDoItems.Remove(itemToRemove);

        }



    }
}
