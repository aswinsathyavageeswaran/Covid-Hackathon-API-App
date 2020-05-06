using AutoMapper;
using HackCovidAPICore.DataAccess;
using HackCovidAPICore.DTO;
using HackCovidAPICore.Enumerators;
using HackCovidAPICore.Model;
using HackCovidAPICore.ResponseModel;
using HackCovidAPICore.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace HackCovidAPICore.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ShopController : ControllerBase
	{
		private IPushNotificationService pushNotificationService;
		private INoteCosmosDB noteCosmosDBService;
		private IUserCosmosDB userCosmosDBService;
		private IMapper mapper;
		public ShopController(IPushNotificationService _pushNotificationService, IMapper _mapper, INoteCosmosDB _noteCosmosDB, IUserCosmosDB _userCosmosDB)
		{
			pushNotificationService = _pushNotificationService;
			mapper = _mapper;
			noteCosmosDBService = _noteCosmosDB;
			userCosmosDBService = _userCosmosDB;
		}

		[HttpPost("changeshopstatus")]
		public async Task<ActionResult> ChangeShopStatus(ShopStatusDTO shopStatusDto)
		{
			ShopModel shop = await userCosmosDBService.GetUserInfo(shopStatusDto.UserEmail);
			if (shop != null)
			{
				shop.Status = shopStatusDto.Status;
				if (await userCosmosDBService.ReplaceDocumentAsync(shop.SelfLink, shop))
					return Ok("Shop status changed successfully");
				return StatusCode(500, "Unable to update the status");
			}
			return BadRequest("Couldn't find the shop specified");
		}

		[HttpPost("shopsnearby")]
		public async Task<ActionResult> GetNearbyShops(ShopsNearbyDTO shopsNearbyDTO)
		{
			List<ShopModel> shops = await userCosmosDBService.GetShops(shopsNearbyDTO.TypeOfBusiness);
			if (shops?.Count > 0)
			{
				ShopsModel shopsModel = new ShopsModel();
				shopsModel.ShopModel = DistanceCalculator.GetDistance(shops, shopsNearbyDTO.Latitude, shopsNearbyDTO.Longitude);
				return Ok(mapper.Map<ResponseModel.Shops>(shopsModel).Shop);
			}
			return Ok("Couldn't find any shops nearby");
		}

		[HttpGet("getbusinesstypes")]
		public ActionResult GetBusinessTypes()
		{
			return Ok(BusinessTypesEnum.BusinessTypes);
		}

		[HttpGet("getallshopnotes")]
		public async Task<ActionResult> GetAllShopOrders(string shopEmail)
		{
			NotesModel notes = new NotesModel();
			notes.NoteModel = await noteCosmosDBService.GetShopNotesAsList(shopEmail);
			return Ok(mapper.Map<NotesInfo>(notes).NoteInfo);
		}

		[HttpPost("confirmnoteitems")]
		public async Task<ActionResult> ConfirmNoteItems(ConfirmNoteItemsDTO availableItems)
		{
			NoteModel note = await noteCosmosDBService.GetNote(availableItems.NoteID);
			if (note != null)
			{
				Model.Shop shop = note.Shops.First(x => x.ShopEmail.Equals(availableItems.UserEmail));
				shop.Notes = mapper.Map<NoteModel>(availableItems).Notes;
				note.Shops.RemoveAll(x => x.ShopEmail.Equals(availableItems.UserEmail));
				shop.ResponseTime = DateTime.Now;
				shop.ShopStatus = 1;
				note.Shops.Add(shop);
				if (await noteCosmosDBService.ReplaceDocumentAsync(note.SelfLink, note))
				{
					string title = $"The shop {shop.ShopName} has responded to your order";
					var data = new Dictionary<string, string>();
					data.Add("orderid", note.Id);
					var notificationData = new NotificationData()
					{
						msgBody = title,
						msgTitle = title,
						tokenList = note.PhoneGuid,
						options = data
					};

					await pushNotificationService.SendNotification(notificationData);
					return Ok("Note Successfully Updated");
				}
				return StatusCode(500, "Unable to update the status");
			}
			return BadRequest("Specified Note doesn't exist");
		}

		[HttpPut("deleteorder")]
		public async Task<ActionResult> DeleteOrder(ConfirmOrderDTO orderDetails)
		{
			NoteModel note = await noteCosmosDBService.GetNote(orderDetails.NoteId);
			if (note != null)
			{
				string shopName = note.Shops.First(x => x.ShopEmail.Equals(orderDetails.ShopEmail)).ShopName;
				note.Shops.RemoveAll(x => x.ShopEmail.Equals(orderDetails.ShopEmail));
				if (await noteCosmosDBService.ReplaceDocumentAsync(note.SelfLink, note))
				{
					string notification = $"The shop {shopName} has cancelled the order";
					var data = new Dictionary<string, string>();
					data.Add("orderid", note.Id);
					var notificationData = new NotificationData()
					{
						msgBody = notification,
						msgTitle = notification,
						tokenList = note.PhoneGuid,
						options = data
					};
					await pushNotificationService.SendNotification(notificationData);
					return Ok("Order Cancelled Successfully");
				}
				return StatusCode(500, "Unable to cancel the order");
			}
			return BadRequest("Specified Note doesn't exist");
		}

		[HttpPost("changeorderstatus")]
		public async Task<ActionResult> ChangeOrderStatus(ShopOrderStatusDTO orderStatus)
		{
			NoteModel note = await noteCosmosDBService.GetNote(orderStatus.NoteId);
			if (note != null)
			{
				string shopName = note.Shops.First(x => x.ShopEmail.Equals(orderStatus.ShopEmail)).ShopName;
				note.Shops.First(x => x.ShopEmail.Equals(orderStatus.ShopEmail)).ShopStatus = orderStatus.Status;
				if (await noteCosmosDBService.ReplaceDocumentAsync(note.SelfLink, note))
				{
					string notification = null;
					if (orderStatus.Status == 2)
						notification = $"The Shop {shopName} has packed your order";
					else if (orderStatus.Status == 3)
						notification = $"The Shop {shopName} has completed your order";
					if (!string.IsNullOrEmpty(notification))
					{
						var data = new Dictionary<string, string>();
						data.Add("orderid", note.Id);
						var notificationData = new NotificationData()
						{
							msgBody = notification,
							msgTitle = notification,
							tokenList = note.PhoneGuid,
							options = data
						};
						await pushNotificationService.SendNotification(notificationData);
					}
					return Ok("Successfully Updated the Shop Status");
				}
				return StatusCode(500, "Unable to change the order status");
			}
			return BadRequest("Specified Note doesn't exist");
		}
	}
}