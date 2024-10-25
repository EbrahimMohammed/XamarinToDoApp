using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using XamarinToDoApp.Models;
using XamarinToDoApp.Persistance;

namespace XamarinToDoApp.ViewModels
{
    public class ToDoDetailsViewModel : INotifyPropertyChanged
    {
        private readonly IToDoItemDetailsStore _toDoItemDetailsStore;

        public ToDoDetailsViewModel(IToDoItemDetailsStore toDoItemDetailsStore)
        {
            _toDoItemDetailsStore = toDoItemDetailsStore;
        }
        public ToDoDetailsViewModel()
        {
            
        }

        public async Task InitializeAsync(ToDoItem item)
        {
            if (item.ToDoItemDetailsId.HasValue)
            {
                ToDoItemDetails = await _toDoItemDetailsStore.GetById(item.ToDoItemDetailsId.Value);
            }
        }
        private ToDoItemDetails _toDoItemDetails;

        public ToDoItemDetails ToDoItemDetails
        {
            get => _toDoItemDetails;
            set
            {
                if (_toDoItemDetails != value)
                {
                    _toDoItemDetails = value;
                    OnPropertyChanged(nameof(ToDoItemDetails));
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
