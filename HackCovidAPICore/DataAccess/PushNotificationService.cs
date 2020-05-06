using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackCovidAPICore.DataAccess
{
	public class PushNotificationService : IPushNotificationService
    {
        private readonly string endPointUrl = "https://locatezdev.web.app/api/v1/sendNotification";
        private HttpClient client;

        public PushNotificationService()
        {
            try
            {
                client = new HttpClient();
            }
            catch { }
        }
       
        public async Task<bool> SendNotification(NotificationData notificationData)
        {
            try
            {
                string json = JsonConvert.SerializeObject(notificationData, Formatting.Indented);
                var httpContent = new StringContent(json);
                httpContent.Headers.Clear();
                httpContent.Headers.Add("Content-Type", "application/json");
                await client.PostAsync(endPointUrl, httpContent);
                return true;
            }
            catch(Exception ex) { }

            return false;
        }

    }

    public class NotificationData
    {
        public object tokenList { get; set; }
        public string msgTitle { get; set; }
        public string msgBody { get; set; }
        public Dictionary<string, string> options { get; set; }
    }
}
