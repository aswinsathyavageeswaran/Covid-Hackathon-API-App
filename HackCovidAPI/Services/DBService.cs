using HackCovidAPI.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace HackCovidAPI.Services
{
	public class DBService : IDBService
	{
		private static IMongoDatabase mongoDBClient;
		public DBService()
		{
			var datasource = ConfigurationManager.AppSettings["DataSource"];
			var mongoUrl = ConfigurationManager.AppSettings["MongoUrl"];
			MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(mongoUrl));
			MongoClient client = new MongoClient(settings);
			mongoDBClient = client.GetDatabase(datasource);
		}

		public List<BusinessType> GetBussinessTypeCollection()
		{
			var businessList = new List<BusinessType>();
			try
			{
				var businessTypeRec = mongoDBClient.GetCollection<BusinessType>("BusinessType");
				businessList = businessTypeRec.AsQueryable().ToList();
			}
			catch { }//Logger
			return businessList;
		}

		public bool ChangeShopStatus(int shopId, int status)
		{
			var success = false;
			try
			{
				var shopRec = mongoDBClient.GetCollection<ShopModel>("ShopInfo");
				var filter = Builders<ShopModel>.Filter.Eq("ShopId", shopId);
				var update = Builders<ShopModel>.Update.Set("Status", status);
				shopRec.UpdateOne(filter, update);
				success = true;
			}
			catch { }
			return success;
		}

		public UserModel GetUserData(string email)
		{
			var userData = new UserModel();

			var userRec = mongoDBClient.GetCollection<UserModel>("UserData");
			var builder = Builders<UserModel>.Filter;
			var filter = builder.Eq("Email", email);// &builder.Eq("Password", password);
			var doc = userRec.Find(filter).FirstOrDefault();
			if (doc != null)
			{
				userData.UserId = doc.UserId;
				userData.UserType = doc.UserType;
				userData.UserName = doc.UserName;
			}
			return userData;
		}

	}
}