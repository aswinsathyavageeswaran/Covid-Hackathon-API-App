using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HackCovidAPICore.DTO;
using HackCovidAPICore.Model;
using Microsoft.Azure.Documents;
using HackCovidAPICore.ResponseModel;

namespace HackCovidAPICore.DataAccess
{
	public interface ICosmosDBService
	{
		Task<bool> Register(ShopModel schema, string password);
		Task<bool> UserExists(string userEmail);
		Task<UserInfo> Login(string userEmail, string password);
		Task<bool> UpdateShopStatus(string userEmail, int status);
		List<ShopModel> GetShopsNearby(double longitude, double latitude, int businessType);
		Task<bool> UpdateProfile(ShopModel schema, string password);
		Task<Document> SaveNote(NoteDTO noteDTO, List<ShopModel> shops);
		Task<List<NoteModel>> GetAllShopNotes(string shopEmail);
		Task<List<NoteModel>> GetAllUserNotes(string phoneNumber);
		Task<Tuple<string, string>> UpdateAvailableItems(ConfirmNoteItemsDTO availableItems);
		Task<string> ConfirmOrderToShop(ConfirmOrderDTO confirmOrder);
		Task<bool> DeleteNote(string noteId);
		Task<Tuple<string, string>> DeleteOrder(string noteId, string shopEmail);
		Task<Tuple<string,string>> CompleteOrder(string noteId);
		Task<string> UpdateShopOrderStatus(string noteId, string shopEmail, int Status);
	}
}
