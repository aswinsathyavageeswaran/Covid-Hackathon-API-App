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
		private List<BusinessTypeModel> businessTypes;
		public ShopController(ICosmosDBService _cosmosDBService)
		{
			cosmosDBService = _cosmosDBService;

			businessTypes = new List<BusinessTypeModel>();
			businessTypes.Add(new BusinessTypeModel { TypeId = 1, Description = "Medical" });
			businessTypes.Add(new BusinessTypeModel { TypeId = 2, Description = "Hotel" });
			businessTypes.Add(new BusinessTypeModel { TypeId = 3, Description = "Shop" });
			businessTypes.Add(new BusinessTypeModel { TypeId = 4, Description = "Petrol Pump" });
			businessTypes.Add(new BusinessTypeModel { TypeId = 5, Description = "Hospital" });
			businessTypes.Add(new BusinessTypeModel { TypeId = 6, Description = "Food Depot" });
			businessTypes.Add(new BusinessTypeModel { TypeId = 7, Description = "Bank" });
			businessTypes.Add(new BusinessTypeModel { TypeId = 8, Description = "Govt. Office" });
		}

		[HttpPost("changeshopstatus")]
		public async Task<ActionResult> ChangeShopStatus(ShopStatusDTO shopStatusDto)
		{
			if (await cosmosDBService.UpdateShopStatus(shopStatusDto.UserEmail, shopStatusDto.Status))
				return Ok("Successfully Updated the Shop Status");
			return BadRequest("Something went Wrong!");
		}

		[HttpGet("shopsnearby")]
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