using HackCovidAPICore.DataAccess;
using HackCovidAPICore.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackCovidAPICore.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ShopController : ControllerBase
	{
		private ICosmosDBService cosmosDBService;
		public ShopController(ICosmosDBService _cosmosDBService)
		{
			cosmosDBService = _cosmosDBService;
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
				return Ok(cosmosDBService.GetShopsNearby(shopsNearbyDTO.longitude, shopsNearbyDTO.latitude));
			}
			catch { }
			return BadRequest("Unable to find any Shops Nearby");
		}
	}
}