using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HackCovidAPICore.DTO;
using HackCovidAPICore.DataAccess;
using HackCovidAPICore.Model;
using System.Collections.Generic;
using System;
using AutoMapper;
using HackCovidAPICore.Utilities;
using HackCovidAPICore.ResponseModel;
using System.Device.Location;
using System.Linq;

namespace HackCovidAPICore.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private IUserCosmosDB userCosmosDBService;
		private INoteCosmosDB noteCosmosDBService;
		private IPushNotificationService pushNotificationService;
		private readonly IMapper mapper;

		public UserController(IPushNotificationService _pushNotificationService, IUserCosmosDB _userCosmosDBService, IMapper _mapper, INoteCosmosDB _noteCosmosDBService)
		{
			pushNotificationService = _pushNotificationService;
			userCosmosDBService = _userCosmosDBService;
			noteCosmosDBService = _noteCosmosDBService;
			mapper = _mapper;
		}

		[HttpPost("register")]
		public async Task<ActionResult> Register(UserForRegisterDTO registerDto)
		{
			if (await userCosmosDBService.UserExists(registerDto.UserEmail.ToLower()))
				return BadRequest("Email ID already exists");

			Encryption.CreatePasswordHash(registerDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

			ShopModel shop = mapper.Map<ShopModel>(registerDto);
			shop.PasswordHash = passwordHash;
			shop.PasswordSalt = passwordSalt;

			if (await userCosmosDBService.CreateDocumentAsync<ShopModel>(shop))
				return Ok("Shop Successfully Registered");
			return StatusCode(500, "Something went wrong");

		}

		[HttpGet("verifyuser")]
		public async Task<ActionResult> VerifyUser(string phoneNumber)
		{
			ShopModel shop = await userCosmosDBService.VerifyUser(phoneNumber);
			if(shop!=null)
				return Ok(mapper.Map<UserInfo>(shop));
			return BadRequest("Invalid User");
		}

		[HttpPost("login")]
		public async Task<ActionResult> Login(UserLoginDTO user)
		{
			ShopModel shop = await userCosmosDBService.GetUserInfo(user.UserEmail.ToLower());

			if (Encryption.VerifyPasswordHash(user.Password, shop))
			{
				return Ok(mapper.Map<UserInfo>(shop));
			}
			return BadRequest("Invalid Username or Password");
		}
		
		[HttpPost("updateprofile")]
		public async Task<ActionResult> UpdateProfile(UpdateProfileDTO updateProfileDTO)
		{

			ShopModel shop = await userCosmosDBService.GetUserInfo(updateProfileDTO.UserEmail);
			mapper.Map(updateProfileDTO, shop, typeof(UpdateProfileDTO), typeof(ShopModel));

			if (!string.IsNullOrWhiteSpace(updateProfileDTO.Password))
			{
				Encryption.CreatePasswordHash(updateProfileDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
				shop.PasswordHash = passwordHash;
				shop.PasswordSalt = passwordSalt;
			}

			if (await userCosmosDBService.ReplaceDocumentAsync(shop.SelfLink, shop))
				return Ok("Successfully updated the profile of the user");
			return BadRequest("Something Went Wrong!");

		}

		[HttpPost("submitnote")]
		public async Task<ActionResult> SubmitNote(NoteDTO noteDTO)
		{
			List<ShopModel> shops = await userCosmosDBService.GetShops(noteDTO.Category);
			if (shops?.Count > 0)
			{
				List<ShopModel> nearbyShops = DistanceCalculator.GetDistance(shops, noteDTO.Latitude, noteDTO.Longitude);
				string body = string.Format("You have received a new order request from {0}", noteDTO.UserPhoneNumber);
				var phoneGuidList = nearbyShops.Select(shop => shop.PhoneGuid);
				var notificationData = new NotificationData()
				{
					msgBody = body,
					msgTitle = "You have received a new order request",
					tokenList = phoneGuidList,
					options = null
				};
				await pushNotificationService.SendNotification(notificationData);
				
				//Mapping
				NoteModel note = mapper.Map<NoteModel>(noteDTO);
				ShopsModel shopsModel = new ShopsModel();
				shopsModel.ShopModel = nearbyShops;
				Model.Shops shopsresult = mapper.Map<Model.Shops>(shopsModel);
				note.Shops = shopsresult.Shop;

				//Assigning fields
				note.NoteTime = DateTime.Now;
				note.Status = 0;

				note = await noteCosmosDBService.CreateAndReturnDocumentAsync(note);
				if (note != null)
					return Ok(mapper.Map<NoteInfo>(note));
			}
			return Ok("No shops found with the matching criteria");
		}

		[HttpGet("getusernotes")]
		public async Task<ActionResult> GetAllUserNotes(string phoneNumber)
		{
			NotesModel notes = new NotesModel();
			notes.NoteModel = await noteCosmosDBService.GetNotes(phoneNumber);
			return Ok(mapper.Map<NotesInfo>(notes).NoteInfo);
		}

		[HttpPost("confirmorder")]
		public async Task<ActionResult> ConfirmOrderToShop(ConfirmOrderDTO confirmOrder)
		{
			NoteModel note = await noteCosmosDBService.GetNote(confirmOrder.NoteId);
			if (note != null)
			{
				note.Shops.First(x => x.ShopEmail.Equals(confirmOrder.ShopEmail)).Accepted = true;
				note.Shops.RemoveAll(x => x.ShopEmail != confirmOrder.ShopEmail);
				note.Status = 1;
				if (await noteCosmosDBService.ReplaceDocumentAsync(note.SelfLink, note))
				{
					string notification = $"The user {note.UserId} has confirmed the order with you";
					var data = new Dictionary<string, string>();
					data.Add("orderid", note.Id);
					var notificationData = new NotificationData()
					{
						msgBody = notification,
						msgTitle = "User has confirmed the order with you",
						tokenList = note.PhoneGuid,
						options = data
					};
					await pushNotificationService.SendNotification(notificationData);
					return Ok("Order Confirmed with the shop");
				}
				return StatusCode(500, "Something went wrong");
			}
			return BadRequest("Couldn't find the note specified");
		}

		[HttpDelete("deletenote")]
		public async Task<ActionResult> DeleteNote(string noteId)
		{
			NoteModel note = await noteCosmosDBService.GetNote(noteId);
			if (note != null)
			{
				if (await noteCosmosDBService.DeleteDocumentAsync(note.SelfLink))
					return Ok("Note Deleted Successfully");
				return StatusCode(500, "Error deleting Note");
			}
			return BadRequest("Couldn't find the note specified");
		}

		[HttpPost("completeorder")]
		public async Task<ActionResult> CompleteOrder(string noteId)
		{
			NoteModel note = await noteCosmosDBService.GetNote(noteId);
			if (note != null)
			{
				note.Status = 2;
				if (await noteCosmosDBService.ReplaceDocumentAsync(note.SelfLink, note))
				{
					string notification = $"The user {note.UserId} has completed the order";
					var data = new Dictionary<string, string>();
					data.Add("orderid", note.Id);
					var notificationData = new NotificationData()
					{
						msgBody = notification,
						msgTitle = "User has completed the order",
						tokenList = note.PhoneGuid,
						options = data
					};
					await pushNotificationService.SendNotification(notificationData);
					return Ok("Order Completed Successfully");
				}
				return StatusCode(500, "Error updating the Status");
			}
			return BadRequest("Couldn't find the note specified");
		}



		[HttpPost("cancelorder")]
		public async Task<ActionResult> CancelOrder(string noteId)
		{
			NoteModel note = await noteCosmosDBService.GetNote(noteId);
			if (note != null)
			{
				note.Status = 3;
				if (await noteCosmosDBService.ReplaceDocumentAsync(note.SelfLink, note))
				{
					string notification = $"The user {note.UserId} has cancelled the order";
					var data = new Dictionary<string, string>();
					data.Add("orderid", note.Id);
					var notificationData = new NotificationData()
					{
						msgBody = notification,
						msgTitle = "User has cancelled the order",
						tokenList = note.PhoneGuid,
						options = data
					};
					await pushNotificationService.SendNotification(notificationData);
					return Ok("Order Cancelled Successfully");
				}
				return StatusCode(500, "Error updating the Status");
			}
			return BadRequest("Couldn't find the note specified");
		}
	}
}