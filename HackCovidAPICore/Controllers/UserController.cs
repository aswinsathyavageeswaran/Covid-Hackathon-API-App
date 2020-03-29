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
		public async Task<ActionResult> UpdateProfile(UserForRegisterDTO registerDto)
		{
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
			if (await cosmosDBService.UpdateProfile(shopModel, registerDto.Password))
				return Ok("Successfully updated the profile of the user");
			return BadRequest("Something Went Wrong!");

		}

	}
}