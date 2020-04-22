using Microsoft.Azure.Cosmos.Spatial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackCovidAPICore.ResponseModel
{
	public class Shop
	{
		public string ShopId { get; set; }
		public string ShopEmail { get; set; }
		public DateTime ResponseTime { get; set; }
		public bool Accepted { get; set; }
		public string PhoneNumber { get; set; }
		public string DeliveryNumber { get; set; }
		public string Address { get; set; }
		public double Distance { get; set; }
		public string ShopName { get; set; }
		public Point Location { get; set; }
		public List<Note> Notes { get; set; }
		public int ShopStatus { get; set; }
		public string PhoneGuid { get; set; }
	}

	public class Shops
	{
		public List<Shop> Shop { get; set; }
	}
}
