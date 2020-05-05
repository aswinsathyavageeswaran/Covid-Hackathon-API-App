using HackCovidAPICore.Model;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace HackCovidAPICore.DataAccess
{
	public class UserCosmosDB : AzureCosmosDBBase, IUserCosmosDB
	{
		public UserCosmosDB(string collectionLink) : base(collectionLink) { }

		public async Task<bool> UserExists(string userEmail)
		{
			var query = $"SELECT * FROM c WHERE c.UserEmail = '{userEmail}'";
			if (await CreateDocumentQuery<ShopModel>(query) != null)
				return true;
			return false;
		}
		public async Task<ShopModel> VerifyUser(string userPhone)
		{
			var query = $"SELECT * FROM c WHERE c.PhoneNumber = '{userPhone}'";
			return await CreateDocumentQuery<ShopModel>(query);
		}

		public async Task<ShopModel> GetUserInfo(string userEmail)
		{
			var query = $"SELECT * FROM c WHERE c.UserEmail = '{userEmail}'";
			return await CreateDocumentQuery<ShopModel>(query);
		}

		public async Task<List<ShopModel>> GetShops(int businessType)
		{
			string query = $"SELECT * FROM c WHERE c.TypeOfBusiness = {businessType}";
			return await CreateDocumentQueryAsList<ShopModel>(query);
		}
	}
}
