using System.Threading.Tasks;
using HackCovidAPICore.Model;

namespace HackCovidAPICore.DataAccess
{
	public interface ICosmosDBService
	{
		Task<bool> Register(ShopModel schema, string password);
		bool UserExists(string userEmail);
	}
}
