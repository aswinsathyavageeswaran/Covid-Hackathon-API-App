using System.Web.Http;
using System.Web.Http.Cors;
using HackCovidAPI.Services;

namespace HackCovidAPI.Controllers
{
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class ShopController : ApiController
	{
		private IDBService DBService;
		public ShopController(IDBService IDBService)
		{
			DBService = IDBService;
		}

		[HttpPost]
		[Route("covid/shop/ChangeShopStatus")]
		public bool ChangeShopStatus(string shopId, int status)
		{
			return DBService.ChangeShopStatus(shopId, status);
		}
	}
}