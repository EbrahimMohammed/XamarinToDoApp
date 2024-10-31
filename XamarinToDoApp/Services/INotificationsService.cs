using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinToDoApp.Services
{
    public interface INotificationsService
    {
        Task ScheduleNotification(int id, string title, string description, DateTime notifyTime);
        Task CancelNotification(int id);
       
    }

}
