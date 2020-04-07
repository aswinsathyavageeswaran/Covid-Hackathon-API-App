using HackCovidAPICore.DataAccess;
using HackCovidAPICore.DTO;
using HackCovidAPICore.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackCovidAPICore.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ShopController : ControllerBase
	{
		private ICosmosDBService cosmosDBService;
		private IPushNotificationService pushNotificationService;
		private List<BusinessTypeModel> businessTypes;
		public ShopController(ICosmosDBService _cosmosDBService, IPushNotificationService _pushNotificationService)
		{
			cosmosDBService = _cosmosDBService;
			pushNotificationService = _pushNotificationService;
			businessTypes = new List<BusinessTypeModel>();
			businessTypes.Add(new BusinessTypeModel { TypeId = 1, Description = "Medical / Pharmacy" });
			businessTypes.Add(new BusinessTypeModel { TypeId = 2, Description = "Groceries / Provision Stores" });
			businessTypes.Add(new BusinessTypeModel { TypeId = 3, Description = "Govt. Covid Help Centers / Hospitals" });
			businessTypes.Add(new BusinessTypeModel { TypeId = 4, Description = "Vegetables / Fruits" });
			businessTypes.Add(new BusinessTypeModel { TypeId = 5, Description = "Petrol Pumps" });
			businessTypes.Add(new BusinessTypeModel { TypeId = 6, Description = "Meat / Diary Products" });
		}

		[HttpPost("changeshopstatus")]
		public async Task<ActionResult> ChangeShopStatus(ShopStatusDTO shopStatusDto)
		{
			if (await cosmosDBService.UpdateShopStatus(shopStatusDto.UserEmail, shopStatusDto.Status))
				return Ok("Successfully Updated the Shop Status");
			return BadRequest("Something went Wrong!");
		}

		[HttpPost("shopsnearby")]
		public ActionResult GetNearbyShops(ShopsNearbyDTO shopsNearbyDTO)
		{
			try
			{
				return Ok(cosmosDBService.GetShopsNearby(shopsNearbyDTO.Longitude, shopsNearbyDTO.Latitude, shopsNearbyDTO.TypeOfBusiness));
			}
			catch { }
			return BadRequest("Unable to find any Shops Nearby");
		}

		[HttpGet("getbusinesstypes")]
		public ActionResult GetBusinessTypes()
		{			
			return Ok(businessTypes);
		}

	}
}