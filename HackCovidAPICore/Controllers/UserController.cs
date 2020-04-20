using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HackCovidAPICore.DTO;
using HackCovidAPICore.DataAccess;
using HackCovidAPICore.Model;
using System.Collections.Generic;
using System;

namespace HackCovidAPICore.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private ICosmosDBService cosmosDBService;
		private IPushNotificationService pushNotificationService;
		public UserController(ICosmosDBService _cosmosDBService, IPushNotificationService _pushNotificationService)
		{
			cosmosDBService = _cosmosDBService;
			pushNotificationService = _pushNotificationService;
		}

		[HttpPost("register")]
		public async Task<ActionResult> Register(UserForRegisterDTO registerDto)
		{
			if (await cosmosDBService.UserExists(registerDto.UserEmail))
				return BadRequest("Username already exists");

			//Should be done in DataAccess Layer
			ShopModel shopModel = new ShopModel();
			shopModel.UserEmail = registerDto.UserEmail;
			shopModel.ShopName = registerDto.ShopName;
			shopModel.FirstName = registerDto.FirstName;
			shopModel.LastName = registerDto.LastName;
			shopModel.TypeOfBusiness = registerDto.TypeOfBusiness;
			shopModel.Location = new Microsoft.Azure.Cosmos.Spatial.Point(registerDto.Longitude, registerDto.Latitude);
			shopModel.DeliveryNumber = registerDto.DeliveryNumber;
			shopModel.StartTime = registerDto.StartTime;
			shopModel.StopTime = registerDto.StopTime;
			shopModel.Address = registerDto.Address;
			shopModel.PhoneNumber = registerDto.Phone;
			shopModel.PhoneGuid = registerDto.PhoneGuid;


			//await teamDataAccess.Register(registerDto.UserName.ToLower(), registerDto.Password);
			if (await cosmosDBService.Register(shopModel, registerDto.Password))
				return Ok("Successfully registered the user");
			return BadRequest("Something Went Wrong!");

		}

		[HttpPost("login")]
		public async Task<ActionResult> Login(UserLoginDTO user)
		{
			return Ok(await cosmosDBService.Login(user.UserEmail, user.Password));
		}

		[HttpPost("updateprofile")]
		public async Task<ActionResult> UpdateProfile(UpdateProfileDTO updateProfileDTO)
		{
			//Should be done in DataAccess Layer
			ShopModel shopModel = new ShopModel();
			shopModel.UserEmail = updateProfileDTO.UserEmail;
			shopModel.ShopName = updateProfileDTO.ShopName;
			shopModel.FirstName = updateProfileDTO.FirstName;
			shopModel.LastName = updateProfileDTO.LastName;
			shopModel.TypeOfBusiness = updateProfileDTO.TypeOfBusiness;
			shopModel.Location = new Microsoft.Azure.Cosmos.Spatial.Point(updateProfileDTO.Longitude, updateProfileDTO.Latitude);
			shopModel.DeliveryNumber = updateProfileDTO.DeliveryNumber;
			shopModel.StartTime = updateProfileDTO.StartTime;
			shopModel.StopTime = updateProfileDTO.StopTime;
			shopModel.Address = updateProfileDTO.Address;
			shopModel.PhoneNumber = updateProfileDTO.Phone;
			shopModel.PhoneGuid = updateProfileDTO.PhoneGuid;


			//await teamDataAccess.Register(registerDto.UserName.ToLower(), registerDto.Password);
			if (await cosmosDBService.UpdateProfile(shopModel, updateProfileDTO.Password))
				return Ok("Successfully updated the profile of the user");
			return BadRequest("Something Went Wrong!");

		}

		[HttpPost("submitnote")]
		public async Task<ActionResult> SubmitNote(NoteDTO noteDTO)
		{
			try
			{
				//Broadcast to shops
				List<ShopModel> shops = cosmosDBService.GetShopsNearby(noteDTO.Longitude, noteDTO.Latitude, noteDTO.Category);
				foreach (ShopModel shop in shops)
				{
					string body = string.Format("You have received a new request from {0}", noteDTO.UserPhoneNumber);
					//await pushNotificationService.SendNotification(shop.PhoneGuid, "You have received a new request", body);
				}
				if (shops != null)
					return Ok(await cosmosDBService.SaveNote(noteDTO, shops));
			}
			catch { }
			return Ok("No shops found with the matching criteria. Please submit by changing the distance");
			//Ok(null)
		}

		[HttpGet("getusernotes")]
		public async Task<ActionResult> GetAllUserNotes(string phoneNumber)
		{
			return Ok(await cosmosDBService.GetAllUserNotes(phoneNumber));
		}

		[HttpPost("confirmorder")]
		public async Task<ActionResult> ConfirmOrderToShop(ConfirmOrderDTO confirmOrder)
		{
			string shopGuid = await cosmosDBService.ConfirmOrderToShop(confirmOrder);
			if (shopGuid != null)
			{
				await pushNotificationService.SendNotification(shopGuid, "You have received a new order", null);
				return Ok("Order placed to the shop");
			}
			return Ok("Order is placed but shop notification note complete");
		}

		[HttpDelete("deletenote")]
		public async Task<ActionResult> DeleteNote(string noteId)
		{
			if (await cosmosDBService.DeleteNote(noteId))
				return Ok("Note Deleted Successfully");
			return Ok("Error deleting Note");
		}

		[HttpPost("completeorder")]
		public async Task<ActionResult> CompleteOrder(string noteId)
		{
			Tuple<string,string> result = await cosmosDBService.CompleteOrder(noteId);
			if (result != null)
			{
				await pushNotificationService.SendNotification(result.Item1, $"{result.Item2} has completed the order", null);
				return Ok("Order completed Successfully");
			}
			return Ok("Unable to complete the order");
		}

	}
}