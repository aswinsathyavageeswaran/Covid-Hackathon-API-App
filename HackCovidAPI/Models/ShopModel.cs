using HackCovidAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;

namespace HackCovidAPI.Models
{
	public class ShopModel
	{
		public object _id { get; set; }
		public int ShopId { get; set; }
		public int TypeOfBusiness { get; set; }
		public string Location { get; set; }
		public int Status { get; set; }
		public Int64 DeliveryNumber { get; set; }
		public string WorkingHours { get; set; }
	}
}