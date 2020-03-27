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

		public ShopModel LoginUser(string email, string password)
		{			
			var userRec = mongoDBClient.GetCollection<ShopModel>("ShopInfo");
			var builder = Builders<ShopModel>.Filter;
			var filter = builder.Eq("Email", email) & builder.Eq("Password", password);
			var doc = userRec.Find(filter).FirstOrDefault();
			if (doc != null)
			{
				doc.Password = "";
				doc.ShopId = doc._id.ToString();
			}
			return doc;
		}

		public int RegisterUser(ShopModel shopData)
		{
			var success = 0;
			try
			{
				var userRec = mongoDBClient.GetCollection<ShopModel>("ShopInfo");
				var filter = Builders<ShopModel>.Filter.Eq("Email", shopData.Email);
				if (userRec.Find(filter).FirstOrDefault() != null)
				{
					success = 1; // user with same email already exists
				}
				else
				{
					shopData.Status = 2; //default to closed
					userRec.InsertOne(shopData);					
				}
			}
			catch
			{
				success = 2; // failure
			}
			return success;
		}

		public int UpdateProfile(ShopModel shopData)
		{
			var success = 0;
			try
			{
				var userRec = mongoDBClient.GetCollection<ShopModel>("ShopInfo");
				var filter = Builders<ShopModel>.Filter.Eq("_id", new ObjectId(shopData.ShopId));
				var updateRec = userRec.Find(filter).FirstOrDefault();
				if (updateRec != null)
				{
					var update = Builders<ShopModel>.Update.
						Set("UserName", shopData.UserName).
						Set("ShopName", shopData.ShopName).
						Set("TypeOfBusiness", shopData.TypeOfBusiness).
						Set("DeliveryNumber", shopData.DeliveryNumber).
						Set("WorkingHours", shopData.WorkingHours).
						Set("Email", shopData.Email).
						Set("Phone", shopData.Phone).
						Set("Address", shopData.Address);
					userRec.UpdateOne(filter, update);					
				}
				else
				{
					success = 1; // user not found
				}
			}
			catch
			{
				success = 2; //failure
			}
			return success;
		}
	}
}