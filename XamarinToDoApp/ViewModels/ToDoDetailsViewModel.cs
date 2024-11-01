using Plugin.LocalNotification;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinToDoApp.Models;
using XamarinToDoApp.Persistance;
using XamarinToDoApp.Services;

namespace XamarinToDoApp.ViewModels
{
    public class ToDoDetailsViewModel : INotifyPropertyChanged
    {
        public DateTime Today => DateTime.Today;

        private readonly IToDoItemDetailsStore _toDoItemDetailsStore;
        private readonly INotificationsService _notificationsService;

        public ToDoDetailsViewModel(IToDoItemDetailsStore toDoItemDetailsStore, INotificationsService notificationsService)
        {
            _toDoItemDetailsStore = toDoItemDetailsStore;
            _notificationsService = notificationsService;
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
            await _notificationsService.CancelNotification(ToDoItemDetails.ToDoItemId);//Ensure if notification created before, delete it
            await ScheduleTaskNotification();
            await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
        }

        public async Task ScheduleTaskNotification()
        {
           
                // Combine DueDate and DueTime to create the notification date
                DateTime notifyDateTime = new DateTime(
                    ToDoItemDetails.DueDate.Year,
                    ToDoItemDetails.DueDate.Month,
                    ToDoItemDetails.DueDate.Day,
                    DueTime.Hours,
                    DueTime.Minutes,
                    DueTime.Seconds
                );

                
                if (notifyDateTime > DateTime.Now.AddMinutes(1))
                {
                // Schedule the notification 1 minute before the due time
                DateTime scheduleTime = notifyDateTime.AddMinutes(-1);
                await _notificationsService.ScheduleNotification(
                        id: ToDoItemDetails.ToDoItemDetailsId, 
                        title: "Task Reminder",
                        description: $"Reminder for task: {ToDoItemDetails.ToDoItem.Text}",
                        notifyTime: scheduleTime 
                    );
                }
            
        }

    }
}
