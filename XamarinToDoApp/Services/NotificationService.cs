using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinToDoApp.Constants;

namespace XamarinToDoApp.Services
{
    public class NotificationService : INotificationsService
    {
        public async Task ScheduleNotification(int id, string title, string description, DateTime notifyTime)
        {
            var notificatinoRequest = new NotificationRequest
            {
                BadgeNumber = 1,
                Description = description,
                Title = title,
                NotificationId = id,
                Android = new Plugin.LocalNotification.AndroidOption.AndroidOptions
                {
                    Priority = Plugin.LocalNotification.AndroidOption.AndroidNotificationPriority.High, 
                    ChannelId = NotificationsConstants.HighImportanceChannelId,
                },
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = notifyTime
                },


            };

            await NotificationCenter.Current.Show(notificatinoRequest);
        }

        public async Task CancelNotification(int id)
        {
             NotificationCenter.Current.Cancel(id);
        }
    }
}
