using HackCovidAPICore.DataAccess;
using HackCovidAPICore.DTO;
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
		private List<string> businessTypes;
		public ShopController(ICosmosDBService _cosmosDBService)
		{
			cosmosDBService = _cosmosDBService;

			businessTypes = new List<string>();
			businessTypes.Add("Medical");
			businessTypes.Add("Hotel");
			businessTypes.Add("Shop");
			businessTypes.Add("Petrol Pump");
			businessTypes.Add("Hospital");
			businessTypes.Add("Food Depot");
			businessTypes.Add("Bank");
			businessTypes.Add("Govt. Office");
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