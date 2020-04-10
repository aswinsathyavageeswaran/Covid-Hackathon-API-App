using System.Net.Http;
using System.Threading.Tasks;

namespace HackCovidAPICore.DataAccess
{
	public class PushNotificationService : IPushNotificationService
    {
        private readonly string endPointUrl = "https://testapp-2b0a4.web.app/api/v1/sendNotification?";
        private HttpClient client;

        public PushNotificationService()
        {
            try
            {
                client = new HttpClient();
            }
            catch { }
        }
        public async Task<bool> SendNotification(string uid, string title, string body)
        {
            try
            {
                var content = string.Format("uid={0}&msgTitle={1}&msgBody={2}", uid, title, body);
                await client.GetAsync(endPointUrl+ content);
                return true;
            }
            catch { }

            return false;
        }

    }
}
