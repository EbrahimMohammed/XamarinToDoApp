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
using XamarinToDoApp.Services;

namespace XamarinToDoApp.ViewModels
{
    public class ToDoListViewModel : INotifyPropertyChanged
    {

        private IToDoItemStore _toDoItemStore;
        private INotificationsService _notificationsService;

        public ObservableCollection<ToDoItem> ToDoItems { get; private set; }
        = new ObservableCollection<ToDoItem>();

        public ToDoListViewModel(IToDoItemStore toDoItemStore, INotificationsService notificationsService)
        {
            _toDoItemStore = toDoItemStore;
            _notificationsService = notificationsService;
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
            var details = new ToDoItemDetails { Details = toDoItem.Text, DueDate = DateTime.Now.AddDays(1), DueTime = DateTime.Now.TimeOfDay };
            NewToDoTextValue = string.Empty; // Clear the Entry text
            _toDoItemStore.AddToDoItemWithDetails(toDoItem, details);
            ToDoItems.Add(toDoItem);
        }


        public ICommand RemoveToDoCommand => new Command(RemoveToDoItem);
        void RemoveToDoItem(object o)
        {
            ToDoItem itemToRemove = o as ToDoItem;
            ToDoItems.Remove(itemToRemove);
            _toDoItemStore.Delete(itemToRemove);
            _notificationsService.CancelNotification(itemToRemove.Id); // ensure notification is deleted
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public async Task LoadData()
        {
            ToDoItems.Clear();
            var toDoItems = await _toDoItemStore.GetAll();
            foreach (var toDoItem in toDoItems)
                ToDoItems.Add(toDoItem);
        }


        public ICommand OnCheckChangedCommand => new Command<ToDoItem>(OnCheckedChanged);

        private void OnCheckedChanged(ToDoItem item)
        {
            _toDoItemStore.Update(item);
            if (item.Completed)
            {
                _notificationsService.CancelNotification(item.Id); // ensure notification is deleted when task marked as completed
            }
        }

        public ICommand NavigateToDetailsCommand => new Command<ToDoItem>(OnNavigateToDetails);

        private async void OnNavigateToDetails(ToDoItem item)
        {
            if (item == null)
                return;
            // Pass the selected ToDoItem to the details page
            await Application.Current.MainPage.Navigation.PushAsync(new ToDoDetailsPage(item));
        }


    }
}
