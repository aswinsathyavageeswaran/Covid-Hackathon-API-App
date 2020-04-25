using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackCovidAPICore.ResponseModel
{
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
