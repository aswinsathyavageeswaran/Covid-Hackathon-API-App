using HackCovidAPI.Models;
using System.Collections.Generic;

namespace HackCovidAPI.Services
{
	public interface IDBService
	{
		List<BusinessType> GetBussinessTypeCollection();
		bool ChangeShopStatus(int shopId, int status);
		UserModel GetUserData(string email);
	}
}