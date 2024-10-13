using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinToDoApp.Models;

namespace XamarinToDoApp.ViewModels
{
    public class ToDoListViewModel : INotifyPropertyChanged
    {


        public ObservableCollection<ToDoItem> ToDoItems { get; set; }

        public ToDoListViewModel()
        {
            ToDoItems = new ObservableCollection<ToDoItem>();
        }

        private string _newToDoTextValue;
        public string NewToDoTextValue
        {
            get => _newToDoTextValue;
            set
            {
                if (_newToDoTextValue != value)
                {
                    _newToDoTextValue = value;
                    OnPropertyChanged(nameof(NewToDoTextValue));
                }
            }
        }
        public ICommand AddNewToDoCommand => new Command(AddNewToDoItem);


        void AddNewToDoItem()
        {
            ToDoItems.Add(new ToDoItem(NewToDoTextValue, false));
            NewToDoTextValue = string.Empty; // Clear the Entry text

        }


        public ICommand RemoveToDoCommand => new Command(RemoveToDoItem);
        void RemoveToDoItem(object o)
        {
            ToDoItem itemToRemove = o as ToDoItem;
            ToDoItems.Remove(itemToRemove);

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}
