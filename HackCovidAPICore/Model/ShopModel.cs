using Newtonsoft.Json;
using System;
using Microsoft.Azure.Cosmos.Spatial;

namespace HackCovidAPICore.Model
{
	public class ShopModel
	{
		[JsonProperty(PropertyName = "_self")]
		public string SelfLink { get; set; }

		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

		[JsonProperty(PropertyName = "PhoneGuid")]
		public string PhoneGuid { get; set; }

		[JsonProperty(PropertyName = "ShopName")]
		public string ShopName { get; set; }

		[JsonProperty(PropertyName = "FirstName")]
		public string FirstName { get; set; }

		[JsonProperty(PropertyName = "LastName")]
		public string LastName { get; set; }

		[JsonProperty(PropertyName = "TypeOfBusiness")]
		public int TypeOfBusiness { get; set; }

		[JsonProperty(PropertyName = "Location")]
		public Point Location { get; set; }

		[JsonProperty(PropertyName = "Status")]
		public int Status { get; set; }

		[JsonProperty(PropertyName = "PhoneNumber")]
		public string PhoneNumber { get; set; }

		[JsonProperty(PropertyName = "DeliveryNumber")]
		public string DeliveryNumber { get; set; }

		[JsonProperty(PropertyName = "StartTime")]
		public DateTime StartTime { get; set; }

		[JsonProperty(PropertyName = "StopTime")]
		public DateTime StopTime { get; set; }

		[JsonProperty(PropertyName = "Address")]
		public string Address { get; set; }

		[JsonProperty(PropertyName = "UserEmail")]
		public string UserEmail { get; set; }

		[JsonProperty(PropertyName = "PasswordSalt")]
		public byte[] PasswordSalt { get; set; }

		[JsonProperty(PropertyName = "PasswordHash")]
		public byte[] PasswordHash { get; set; }

		[JsonProperty(PropertyName = "Distance")]
		public double Distance { get; set; }
	}

	public class UserInfo
	{
		[JsonProperty(PropertyName = "ShopName")]
		public string ShopName { get; set; }

		[JsonProperty(PropertyName = "FirstName")]
		public string FirstName { get; set; }

		[JsonProperty(PropertyName = "LastName")]
		public string LastName { get; set; }

		[JsonProperty(PropertyName = "TypeOfBusiness")]
		public int TypeOfBusiness { get; set; }

		[JsonProperty(PropertyName = "Longitude")]
		public double Longitude { get; set; }

		[JsonProperty(PropertyName = "Latitude")]
		public double Latitude { get; set; }

		[JsonProperty(PropertyName = "Status")]
		public int Status { get; set; }

		[JsonProperty(PropertyName = "DeliveryNumber")]
		public string DeliveryNumber { get; set; }

		[JsonProperty(PropertyName = "Phone")]
		public string Phone { get; set; }

		[JsonProperty(PropertyName = "StartTime")]
		public DateTime StartTime { get; set; }

		[JsonProperty(PropertyName = "StopTime")]
		public DateTime StopTime { get; set; }

		[JsonProperty(PropertyName = "Address")]
		public string Address { get; set; }

		[JsonProperty(PropertyName = "UserEmail")]
		public string UserEmail { get; set; }
	}
}
