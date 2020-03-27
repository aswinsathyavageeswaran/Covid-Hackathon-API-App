using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using HackCovidAPI.Models;
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

		[HttpPost]
		[Route("covid/shop/LoginUser")]
		public dynamic LoginUser(string email, string password)
		{
			return DBService.LoginUser(email, password);
		}

		[HttpPost]
		[Route("covid/shop/RegisterUser")]
		public int RegisterUser(ShopModel shopData)
		{
			return DBService.RegisterUser(shopData);
		}

		[HttpPost]
		[Route("covid/shop/UpdateProfile")]
		public int UpdateUser(ShopModel shopData)
		{
			return DBService.UpdateProfile(shopData);
		}

		[HttpGet]
		[Route("covid/shop/GetNearByShops")]
		public List<ShopModel> GetNearByShops(double latitude, double longitude)
		{
			return DBService.GetNearByShops(latitude, longitude);
		}
	}
}