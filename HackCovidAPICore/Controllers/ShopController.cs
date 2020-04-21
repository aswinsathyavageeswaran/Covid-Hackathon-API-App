using HackCovidAPICore.DataAccess;
using HackCovidAPICore.DTO;
using HackCovidAPICore.Model;
using Microsoft.AspNetCore.Mvc;
using System;
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

		[HttpGet("getallshopnotes")]
		public async Task<ActionResult> GetAllShopOrders(string shopEmail)
		{
			return Ok(await cosmosDBService.GetAllShopNotes(shopEmail));
		}

		[HttpPost("confirmnoteitems")]
		public async Task<ActionResult> ConfirmNoteItems(ConfirmNoteItemsDTO availableItems)
		{
			Tuple<string, string> details = await cosmosDBService.UpdateAvailableItems(availableItems);
			if (details != null)
			{
				string title = $"The shop {details.Item1} has responded to your order";
				await pushNotificationService.SendNotification(details.Item2, title, title);
				return Ok("Note Successfully Updated");
			}
			return NoContent();
		}

		[HttpDelete("deleteorder")]
		public async Task<ActionResult> DeleteOrder(ConfirmOrderDTO orderDetails)
		{
			Tuple<string, string> result = await cosmosDBService.DeleteOrder(orderDetails.NoteId, orderDetails.ShopEmail);
			if (result != null)
			{
				string notification = $"The shop {result.Item2} has cancelled the order";
				await pushNotificationService.SendNotification(result.Item1, notification, notification);
				return Ok("Order Cancelled Successfully");
			}
			return Ok("Unable to cancel the order");
		}

		[HttpPost("changeorderstatus")]
		public async Task<ActionResult> ChangeOrderStatus(ShopOrderStatusDTO orderStatus)
		{
			string userGuid = await cosmosDBService.UpdateShopOrderStatus(orderStatus.NoteId, orderStatus.ShopEmail, orderStatus.Status);
			if (userGuid != null)
			{
				string notification = null;
				if (orderStatus.Status == 2)
					notification = "Shop has packed your order";
				else if (orderStatus.Status == 3)
					notification = "Shop has completed your order";
				if (!string.IsNullOrEmpty(notification))
					await pushNotificationService.SendNotification(userGuid, notification, notification);
				return Ok("Successfully Updated the Shop Status");
			}
			return BadRequest("Something went Wrong!");
		}
	}
}