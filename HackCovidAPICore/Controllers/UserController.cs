﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HackCovidAPICore.DTO;
using HackCovidAPICore.DataAccess;
using HackCovidAPICore.Model;

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
			if (cosmosDBService.UserExists(registerDto.UserEmail))
				return BadRequest("Username already exists");

			//Serialization
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


			//await teamDataAccess.Register(registerDto.UserName.ToLower(), registerDto.Password);
			await cosmosDBService.Register(shopModel, registerDto.Password);
			return Ok("Successfully registered the user");

		}

		[HttpGet("login")]
		public ActionResult Login()
		{
			return Ok("working");
		}

	}
}