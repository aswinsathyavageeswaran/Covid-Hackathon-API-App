﻿using System.Collections.Generic;
using System.Threading.Tasks;
using HackCovidAPICore.Model;

namespace HackCovidAPICore.DataAccess
{
	public interface ICosmosDBService
	{
		Task<bool> Register(ShopModel schema, string password);
		Task<bool> UserExists(string userEmail);
		Task<bool> Login(string userEmail, string password);
		Task<bool> UpdateShopStatus(string userEmail, int status);
		List<ShopModel> GetShopsNearby(double longitude, double latitude, int businessType);
		Task<bool> UpdateProfile(ShopModel schema, string password);
	}
}
