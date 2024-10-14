using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinToDoApp.Models;
using XamarinToDoApp.Persistance;

namespace XamarinToDoApp.ViewModels
{
    public class ToDoListViewModel : INotifyPropertyChanged
    {

        private IToDoItemStore _toDoItemStore;


        public ObservableCollection<ToDoItem> ToDoItems { get; private set; }
        = new ObservableCollection<ToDoItem>();

        public ToDoListViewModel(IToDoItemStore toDoItemStore)
        {
            _toDoItemStore = toDoItemStore;
        }

        public ToDoListViewModel()
        {
            
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
            var toDoItem = new ToDoItem { Completed = false, Text = NewToDoTextValue };
            NewToDoTextValue = string.Empty; // Clear the Entry text
            _toDoItemStore.Add(toDoItem);
            ToDoItems.Add(toDoItem);
        }


        public ICommand RemoveToDoCommand => new Command(RemoveToDoItem);
        void RemoveToDoItem(object o)
        {
            ToDoItem itemToRemove = o as ToDoItem;
            ToDoItems.Remove(itemToRemove);
            _toDoItemStore.Delete(itemToRemove);

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public async Task LoadData()
        {

            var toDoItems = await _toDoItemStore.GetAll();
            foreach (var toDoItem in toDoItems)
                ToDoItems.Add(toDoItem);
        }


        public ICommand OnCheckChangedCommand => new Command<ToDoItem>(OnCheckedChanged);

        private void OnCheckedChanged(ToDoItem item)
        {
            _toDoItemStore.Update(item);
        }


    }
}
