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
using HackCovidAPICore.DTO;
using Microsoft.Azure.Cosmos.Spatial;
using Microsoft.Azure.Documents;
using Newtonsoft.Json.Linq;

namespace HackCovidAPICore.DataAccess
{
	public class CosmosDBService
	{
		//private readonly string endPointUrl = "https://covidcosmosdb.documents.azure.com:443/";
		//private readonly string primaryKey = "jSSmUe7Q6roHGd8j42YhVCkH9or3lP1rM2IKbkIocWF0NDLlrzp4TQaOldRHYwky9l23nAL6nSiRyULP6000kQ==";
		//private readonly string collectionLink = "dbs/S5EfAA==/colls/S5EfAJd6FqA=";
		//private readonly string noteCollectionLink = "dbs/S5EfAA==/colls/S5EfAMyghQo=";

		//private DocumentClient client;

		//public CosmosDBService()
		//{
		//	try
		//	{
		//		client = new DocumentClient(new Uri(endPointUrl), primaryKey);
		//	}
		//	catch { }
		//}

		
		

		//public async Task<UserInfo> Login(string userEmail, string password)
		//{
		//	try
		//	{
		//		var query = client.CreateDocumentQuery<ShopModel>(collectionLink, new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
		//								.Where(r => r.UserEmail == userEmail).AsDocumentQuery();
		//		if (query.HasMoreResults)
		//		{
		//			var results = await query.ExecuteNextAsync<ShopModel>();
		//			if (results.Any())
		//			{
		//				ShopModel shopModel = results.ToList().First();
		//				if (VerifyPasswordHash(password, shopModel))
		//				{
		//					UserInfo userInfo = new UserInfo();
		//					userInfo.Address = shopModel.Address;
		//					userInfo.DeliveryNumber = shopModel.DeliveryNumber;
		//					userInfo.FirstName = shopModel.FirstName;
		//					userInfo.LastName = shopModel.LastName;
		//					userInfo.Latitude = shopModel.Location.Position.Latitude;
		//					userInfo.Longitude = shopModel.Location.Position.Longitude;
		//					userInfo.ShopName = shopModel.ShopName;
		//					userInfo.StartTime = shopModel.StartTime;
		//					userInfo.StopTime = shopModel.StopTime;
		//					userInfo.TypeOfBusiness = shopModel.TypeOfBusiness;
		//					userInfo.UserEmail = shopModel.UserEmail;
		//					userInfo.Phone = shopModel.PhoneNumber;
		//					userInfo.Status = shopModel.Status;
		//					return userInfo;
		//				}
		//			}
		//		}
		//	}
		//	catch { }
		//	return null;
		//}

		//public async Task<bool> UpdateShopStatus(string userEmail, int status)
		//{
		//	try
		//	{
		//		var query = client.CreateDocumentQuery<ShopModel>(collectionLink, new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
		//								.Where(r => r.UserEmail == userEmail).AsDocumentQuery();
		//		if (query.HasMoreResults)
		//		{
		//			var results = await query.ExecuteNextAsync<ShopModel>();
		//			if (results.Any())
		//			{
		//				ShopModel result = results.ToList().First();
		//				result.Status = status;
		//				await client.ReplaceDocumentAsync(result.SelfLink, result);
		//				return true;
		//			}
		//		}
		//	}
		//	catch { }
		//	return false;
		//}

		////Pending to add async
		

		

		//public async Task<Document> SaveNote(NoteDTO noteDTO, List<ShopModel> shops)
		//{
		//	try
		//	{
		//		NoteModel noteModel = new NoteModel();
		//		noteModel.UserId = noteDTO.UserPhoneNumber;
		//		noteModel.Category = noteDTO.Category;
		//		noteModel.SubCategory = noteDTO.SubCategory;
		//		noteModel.NoteTime = DateTime.Now;
		//		noteModel.Status = 0;
		//		noteModel.PhoneGuid = noteDTO.PhoneGuid;
		//		noteModel.Location = new Point(noteDTO.Longitude, noteDTO.Latitude);
		//		noteModel.Notes = new List<Model.Note>();
		//		noteModel.Shops = new List<Shop>();

		//		foreach (DTO.Note note in noteDTO.Notes)
		//		{
		//			Model.Note note1 = new Model.Note();
		//			note1.Description = note.Description;
		//			note1.Metric = note.Metric;
		//			note1.Quantity = note.Quantity;
		//			noteModel.Notes.Add(note1);
		//		}

		//		noteModel.Shops = new List<Shop>();
		//		foreach (ShopModel shop in shops)
		//		{
		//			Shop shop1 = new Shop();
		//			shop1.ShopEmail = shop.UserEmail;
		//			shop1.Address = shop.Address;
		//			shop1.PhoneNumber = shop.PhoneNumber;
		//			shop1.DeliveryNumber = shop.DeliveryNumber;
		//			shop1.ShopName = shop.ShopName;
		//			shop1.Distance = shop.Distance;
		//			shop1.Location = shop.Location;
		//			shop1.Accepted = false;
		//			shop1.ShopStatus = 0;
		//			noteModel.Shops.Add(shop1);
		//		}

		//		Document document = await client.CreateDocumentAsync(noteCollectionLink, noteModel, null, false);
		//		return document;
		//	}
		//	catch { }//Error Logging
		//	return null;
		//}

		//public async Task<List<NoteModel>> GetAllShopNotes(string shopEmail)
		//{
		//	try
		//	{
		//		string queryString = $"SELECT c.id FROM c JOIN s IN c.Shops WHERE s.ShopEmail = '{shopEmail}'";
		//		var query = client.CreateDocumentQuery(collectionLink: noteCollectionLink, sqlExpression: queryString, feedOptions: new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true }).AsDocumentQuery();
		//		if (query.HasMoreResults)
		//		{
		//			var results = await query.ExecuteNextAsync();
		//			List<NoteModel> notes = new List<NoteModel>();
		//			if (results.Any())
		//			{
		//				foreach (JObject jObject in results.ToList())
		//				{
		//					NoteModel note = GetNote((string)jObject["id"]);
		//					note.Shops.RemoveAll(x => x.ShopEmail != shopEmail);
		//					notes.Add(note);
		//				}

		//				return notes;
		//			}
		//		}
		//	}
		//	catch (Exception ex) { }
		//	return null;
		//}

		//private NoteModel GetNote(string id)
		//{
		//	try
		//	{
		//		NoteModel doc = client.CreateDocumentQuery<NoteModel>(noteCollectionLink, new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
		//								.Where(r => r.Id == id)
		//								.AsEnumerable()
		//								.SingleOrDefault();
		//		return doc;
		//	}
		//	catch { }
		//	return null;
		//}

		//public async Task<List<NoteModel>> GetAllUserNotes(string phoneNumber)
		//{
		//	List<NoteModel> notes = new List<NoteModel>();
		//	try
		//	{
		//		string queryString = $"SELECT * FROM c WHERE c.UserId = '{phoneNumber}'";
		//		var query = client.CreateDocumentQuery<NoteModel>(collectionLink: noteCollectionLink, sqlExpression: queryString, feedOptions: new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true }).AsDocumentQuery();
		//		if (query.HasMoreResults)
		//		{
		//			var results = await query.ExecuteNextAsync<NoteModel>();
		//			if (results.Any())
		//			{
		//				notes = results.ToList();
		//			}
		//		}
		//	}
		//	catch { }
		//	return notes;
		//}

		//public async Task<Tuple<string, string>> UpdateAvailableItems(ConfirmNoteItemsDTO availableItems)
		//{
		//	try
		//	{
		//		NoteModel note = GetNote(availableItems.NoteID);
		//		Shop shop = note.Shops.First(x => x.ShopEmail.Equals(availableItems.UserEmail));
		//		shop.Notes = new List<Model.Note>();
		//		foreach (DTO.Note item in availableItems.Notes)
		//		{
		//			Model.Note confirmedItem = new Model.Note();
		//			confirmedItem.Description = item.Description;
		//			confirmedItem.Metric = item.Metric;
		//			confirmedItem.Quantity = item.Quantity;
		//			shop.Notes.Add(confirmedItem);
		//		}
		//		note.Shops.RemoveAll(x => x.ShopEmail.Equals(availableItems.UserEmail));
		//		shop.ResponseTime = DateTime.Now;
		//		shop.ShopStatus = 1;
		//		note.Shops.Add(shop);
		//		await client.ReplaceDocumentAsync(note.SelfLink, note);
		//		return new Tuple<string, string>(shop.ShopName, note.PhoneGuid);
		//	}
		//	catch { }//Error Logging
		//	return null;
		//}

		//public async Task<string> ConfirmOrderToShop(ConfirmOrderDTO confirmOrder)
		//{
		//	try
		//	{
		//		NoteModel note = GetNote(confirmOrder.NoteId);
		//		note.Shops.First(x => x.ShopEmail.Equals(confirmOrder.ShopEmail)).Accepted = true;
		//		note.Shops.RemoveAll(x => x.ShopEmail != confirmOrder.ShopEmail);
		//		note.Status = 1;
		//		/*Important - need to get shop guid ocne registration is complete */
		//		string shopGuid = "guid"; //need to get shopGuid

		//		await client.ReplaceDocumentAsync(note.SelfLink, note);
		//		return shopGuid;
		//	}
		//	catch { }//Error Logging
		//	return null;
		//}

		//public async Task<Tuple<string, string>> DeleteOrder(string noteId, string shopEmail)
		//{
		//	try
		//	{
		//		NoteModel note = GetNote(noteId);
		//		string shopName = note.Shops.First(x => x.ShopEmail.Equals(shopEmail)).ShopName;
		//		note.Shops.RemoveAll(x => x.ShopEmail.Equals(shopEmail));
		//		await client.ReplaceDocumentAsync(note.SelfLink, note);
		//		return new Tuple<string, string>(note.PhoneGuid, shopName);
		//	}
		//	catch { }
		//	return null;
		//}

		//public async Task<Tuple<string, string>> CompleteOrder(string noteId)
		//{
		//	try
		//	{
		//		NoteModel note = GetNote(noteId);
		//		note.Status = 2;
		//		//IMPORTANT : need to send back guid
		//		await client.ReplaceDocumentAsync(note.SelfLink, note);
		//		return new Tuple<string, string>("guid",note.UserId);
		//	}
		//	catch { }//Error Logging
		//	return null;
		//}
		//public async Task<string> UpdateShopOrderStatus(string noteId, string shopEmail, int Status)
		//{
		//	try
		//	{
		//		NoteModel note = GetNote(noteId);
		//		note.Shops.First(x => x.ShopEmail.Equals(shopEmail)).ShopStatus = Status;
		//		await client.ReplaceDocumentAsync(note.SelfLink, note);
		//		return note.PhoneGuid;
		//	}
		//	catch { }//Error Logging
		//	return null;
		//}
	}
}
