using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackCovidAPICore.ResponseModel
{
	public class UserInfo
	{
		public string ShopName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int TypeOfBusiness { get; set; }
		public double Longitude { get; set; }
		public double Latitude { get; set; }
		public int Status { get; set; }
		public string DeliveryNumber { get; set; }
		public string Phone { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime StopTime { get; set; }
		public string Address { get; set; }
		public string UserEmail { get; set; }
	}
}
