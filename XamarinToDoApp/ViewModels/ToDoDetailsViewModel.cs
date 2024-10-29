using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinToDoApp.Models;
using XamarinToDoApp.Persistance;

namespace XamarinToDoApp.ViewModels
{
    public class ToDoDetailsViewModel : INotifyPropertyChanged
    {
        public DateTime Today => DateTime.Today;

        private readonly IToDoItemDetailsStore _toDoItemDetailsStore;

        public ToDoDetailsViewModel(IToDoItemDetailsStore toDoItemDetailsStore)
        {
            _toDoItemDetailsStore = toDoItemDetailsStore;
        }

        public ToDoDetailsViewModel() { }

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

                    // Initialize DueDate and DueTime with model values
                    DueTime = _toDoItemDetails.DueTime;

                    DueDate = _toDoItemDetails.DueDate;
                }
            }
        }

        private DateTime _dueDate; // Changed to nullable
        public DateTime DueDate
        {
            get => _dueDate;
            set
            {
                if (_dueDate != value)
                {
                    _dueDate = value;
                    OnPropertyChanged(nameof(DueDate));

                    // Check if the new DueDate is today
                    if (_dueDate.Date == DateTime.Today)
                    {
                        // If the DueTime is before the current time, set it to the current time
                        if (_dueTime < DateTime.Now.TimeOfDay)
                        {
                            DueTime = DateTime.Now.TimeOfDay; // This will trigger the setter for DueTime
                        }
                    }

                    if (ToDoItemDetails != null)
                        ToDoItemDetails.DueDate = _dueDate;

                }
            }
        }

        private TimeSpan _dueTime; // Changed to nullable
        public TimeSpan DueTime
        {
            get => _dueTime;
            set
            {
                if (_dueTime != value)
                {

                    _dueTime = value;

                    if (_dueDate == DateTime.Today && value < DateTime.Now.TimeOfDay)
                    {
                        _dueTime = DateTime.Now.TimeOfDay;
                    }

                    OnPropertyChanged(nameof(DueTime));

                    if (ToDoItemDetails != null)
                        ToDoItemDetails.DueTime = _dueTime;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand SaveCommand => new Command(OnSaveDetails);

        private async void OnSaveDetails()
        {
            await _toDoItemDetailsStore.Update(ToDoItemDetails);
            await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
        }
    }
}
