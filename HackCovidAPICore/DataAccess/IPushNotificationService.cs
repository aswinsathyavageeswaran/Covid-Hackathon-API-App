using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackCovidAPICore.DataAccess
{
    public interface IPushNotificationService
    {
        Task<bool> SendNotification(NotificationData notificationData);
    }
}
