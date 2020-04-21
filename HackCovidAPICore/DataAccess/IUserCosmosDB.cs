using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackCovidAPICore.Model;

namespace HackCovidAPICore.DataAccess
{
	public interface IUserCosmosDB
	{
		Task<bool> UserExists(string userEmail);
		Task<bool> CreateDocumentAsync<ShopModel>(ShopModel schema);
		Task<ShopModel> GetUserInfo(string userEmail);
		Task<bool> ReplaceDocumentAsync<ShopModel>(string selfLink, ShopModel schema);
		Task<List<ShopModel>> GetShops(int businessType);
	}
}
