using Microsoft.Azure.Cosmos.Spatial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackCovidAPICore.ResponseModel
{
	public class NoteInfo
	{
		public string Id { get; set; }
		public string UserId { get; set; }
		public DateTime NoteTime { get; set; }
		public int Status { get; set; }
		public List<Note> Notes { get; set; }
		public List<Shop> Shops { get; set; }
		public int Category { get; set; }
		public int SubCategory { get; set; }
		public Point Location { get; set; }
	}

	public class Shop
	{
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

	public class Note
	{
		public string Description { get; set; }
		public string Quantity { get; set; }
		public string Metric { get; set; }
	}
}
