using Microsoft.Azure.Cosmos.Spatial;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackCovidAPICore.ResponseModel
{
	public class Shop
	{

		[JsonProperty(PropertyName = "ShopId")]
		public string ShopId { get; set; }

		[JsonProperty(PropertyName = "ShopEmail")]
		public string ShopEmail { get; set; }

		[JsonProperty(PropertyName = "ResponseTime")]
		public DateTime ResponseTime { get; set; }

		[JsonProperty(PropertyName = "Accepted")]
		public bool Accepted { get; set; }

		[JsonProperty(PropertyName = "PhoneNumber")]
		public string PhoneNumber { get; set; }

		[JsonProperty(PropertyName = "DeliveryNumber")]
		public string DeliveryNumber { get; set; }

		[JsonProperty(PropertyName = "Address")]
		public string Address { get; set; }

		[JsonProperty(PropertyName = "Distance")]
		public double Distance { get; set; }

		[JsonProperty(PropertyName = "ShopName")]
		public string ShopName { get; set; }

		[JsonProperty(PropertyName = "Location")]
		public Point Location { get; set; }

		[JsonProperty(PropertyName = "Notes")]
		public List<Note> Notes { get; set; }

		[JsonProperty(PropertyName = "ShopStatus")]
		public int ShopStatus { get; set; }

		[JsonProperty(PropertyName = "PhoneGuid")]
		public string PhoneGuid { get; set; }
	}

	public class Shops
	{

		[JsonProperty(PropertyName = "Shop")]
		public List<Shop> Shop { get; set; }
	}
}
