using HackCovidAPI.Models;
using MongoDB.Bson;
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

		public bool ChangeShopStatus(string shopId, int status)
		{
			var success = false;
			try
			{
				var shopRec = mongoDBClient.GetCollection<ShopModel>("ShopInfo");
				var filter = Builders<ShopModel>.Filter.Eq("_id", new ObjectId(shopId));
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
				userData._id = doc._id;
				userData.UserType = doc.UserType;
				userData.UserName = doc.UserName;
			}
			return userData;
		}

		public int RegisterUser(RegistrationModel registrationDetails)
		{
			var success = 0;
			var userRec = mongoDBClient.GetCollection<UserModel>("UserData");
			var builder = Builders<UserModel>.Filter;
			var filter = builder.Eq("Phone", registrationDetails.UserData.Phone);
			if (userRec.Find(filter).FirstOrDefault() != null)
			{
				success = 1;
			}
			else
			{
				userRec.InsertOne(registrationDetails.UserData);
				if (registrationDetails.UserData.UserType == 2)
				{
					var insertedRec = userRec.Find(filter).FirstOrDefault();
					var shopRec = mongoDBClient.GetCollection<ShopModel>("ShopInfo");
					registrationDetails.ShopData._id = insertedRec._id;
					registrationDetails.ShopData.Status = 2;
					shopRec.InsertOne(registrationDetails.ShopData);
				}
			}
			return success;
		}
	}
}