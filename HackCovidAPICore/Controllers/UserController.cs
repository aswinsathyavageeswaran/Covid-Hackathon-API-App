using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HackCovidAPICore.DTO;
using HackCovidAPICore.DataAccess;
using HackCovidAPICore.Model;
using System.Collections.Generic;

namespace HackCovidAPICore.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private ICosmosDBService cosmosDBService;
		public UserController(ICosmosDBService _cosmosDBService)
		{
			cosmosDBService = _cosmosDBService;
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


			//await teamDataAccess.Register(registerDto.UserName.ToLower(), registerDto.Password);
			if (await cosmosDBService.UpdateProfile(shopModel, updateProfileDTO.Password))
				return Ok("Successfully updated the profile of the user");
			return BadRequest("Something Went Wrong!");

		}

		[HttpPost("submitnote")]
		public async Task<ActionResult> SubmitNote(NoteDTO noteDTO)
		{
			return Ok(await cosmosDBService.SaveNote(noteDTO));
		}

	}
}