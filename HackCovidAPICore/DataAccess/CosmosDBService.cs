using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using HackCovidAPICore.Model;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Azure.Documents.Linq;
using System.Collections.Generic;
using System.Device.Location;

namespace HackCovidAPICore.DataAccess
{
	public class CosmosDBService : ICosmosDBService
	{
		private readonly string endPointUrl = "https://covidcosmosdb.documents.azure.com:443/";
		private readonly string primaryKey = "jSSmUe7Q6roHGd8j42YhVCkH9or3lP1rM2IKbkIocWF0NDLlrzp4TQaOldRHYwky9l23nAL6nSiRyULP6000kQ==";
		private readonly string collectionLink = "dbs/S5EfAA==/colls/S5EfAJd6FqA=";

		private DocumentClient client;

		public CosmosDBService()
		{
			try
			{
				client = new DocumentClient(new Uri(endPointUrl), primaryKey);
			}
			catch { }
		}

		public async Task<bool> Register(ShopModel schema, string password)
		{
			try
			{
				schema.Status = 0; //Set Shop as closed by default
				CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
				schema.PasswordHash = passwordHash;
				schema.PasswordSalt = passwordSalt;
				await client.CreateDocumentAsync(collectionLink, schema, null, false);
				return true;
			}
			catch { }//Error Logging
			return false;
		}

		public async Task<bool> UserExists(string userEmail)
		{
			try
			{
				var query = client.CreateDocumentQuery<ShopModel>(collectionLink, new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
										.Where(r => r.UserEmail == userEmail).AsDocumentQuery();
				if (query.HasMoreResults)
				{
					var results = await query.ExecuteNextAsync<ShopModel>();
					if (results.Any())
					{
						return true;
					}
				}
			}
			catch { }
			return false;
		}

		public async Task<bool> Login(string userEmail, string password)
		{
			try
			{
				var query = client.CreateDocumentQuery<ShopModel>(collectionLink, new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
										.Where(r => r.UserEmail == userEmail).AsDocumentQuery();
				if (query.HasMoreResults)
				{
					var results = await query.ExecuteNextAsync<ShopModel>();
					if (results.Any())
					{
						ShopModel shopModel = results.ToList().First();
						return VerifyPasswordHash(password, shopModel);
					}
				}
			}
			catch { }
			return false;
		}

		public async Task<bool> UpdateShopStatus(string userEmail, int status)
		{
			try
			{
				var query = client.CreateDocumentQuery<ShopModel>(collectionLink, new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
										.Where(r => r.UserEmail == userEmail).AsDocumentQuery();
				if (query.HasMoreResults)
				{
					var results = await query.ExecuteNextAsync<ShopModel>();
					if (results.Any())
					{
						ShopModel result = results.ToList().First();
						result.Status = status;
						await client.ReplaceDocumentAsync(result.SelfLink, result);
						return true;
					}
				}
			}
			catch { }
			return false;
		}

		//Pending to add async
		public List<ShopModel> GetShopsNearby(double longitude, double latitude, int businessType)
		{
			try
			{
				//var query = client.CreateDocumentQuery<ShopModel>(collectionLink, new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
				//						.Where(x => x.Location.Distance(new Point(longitude, latitude)) < 10000).AsDocumentQuery();

				var coord = new GeoCoordinate(latitude, longitude);
				List<ShopModel> shopList = client.CreateDocumentQuery<ShopModel>(collectionLink, new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true }).Where(x=>x.TypeOfBusiness == businessType).AsEnumerable().ToList();
				shopList.ForEach(x =>
				{
					x.Distance = Math.Round((new GeoCoordinate(x.Location.Position.Latitude, x.Location.Position.Longitude).GetDistanceTo(coord)) * 1.60934 / 1000, 2);
				});
				List<ShopModel> nearbyShops = shopList.Where(x => x.Distance <10).OrderBy(x => x.Distance).OrderBy(x => x.Status).ToList();
				return nearbyShops;
			}
			catch{ }
			return null;
		}

		private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
			}
		}

		private bool VerifyPasswordHash(string password, ShopModel shopModel)
		{
			using (var hmac = new HMACSHA512(shopModel.PasswordSalt))
			{
				var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
				for (int i = 0; i < computedHash.Length; i++)
				{
					if (computedHash[i] != shopModel.PasswordHash[i])
						return false;
				}
			}
			return true;
		}
	}
}
