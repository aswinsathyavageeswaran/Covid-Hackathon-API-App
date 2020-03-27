using HackCovidAPI.Models;
using System.Collections.Generic;

namespace HackCovidAPI.Services
{
	public interface IDBService
	{
		List<BusinessType> GetBussinessTypeCollection();
		bool ChangeShopStatus(string shopId, int status);
		ShopModel LoginUser(string email, string password);
		int RegisterUser(ShopModel shopData);
		int UpdateProfile(ShopModel shopData);
		List<ShopModel> GetNearByShops(double latitude, double longitude);
	}
}