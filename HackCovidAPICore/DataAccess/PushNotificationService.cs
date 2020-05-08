using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
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
       
        public bool SendNotification(NotificationData notificationData)
        {
            try
            {
                new Thread(async () =>
                {
                    Thread.CurrentThread.IsBackground = true;                   
                    string json = JsonConvert.SerializeObject(notificationData, Formatting.Indented);
                    var httpContent = new StringContent(json);  
                    httpContent.Headers.Clear();
                    httpContent.Headers.Add("Content-Type", "application/json");
                    await client.PostAsync(endPointUrl, httpContent);
                }).Start();
               
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
        //public Dictionary<string, string> options { get; set; }
    }

}
