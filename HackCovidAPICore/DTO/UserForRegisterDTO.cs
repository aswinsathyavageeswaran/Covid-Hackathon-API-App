using System;
using System.ComponentModel.DataAnnotations;

namespace HackCovidAPICore.DTO
{
	public class UserForRegisterDTO
	{
		[Required]
		[EmailAddress]
		public string UserEmail { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		public string ShopName { get; set; }

		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required]
		public string TypeOfBusiness { get; set; }

		public double Longitude { get; set; }

		public double Latitude { get; set; }

		[Phone]
		public string DeliveryNumber { get; set; }

		[Required]
		public DateTime StartTime { get; set; }

		[Required]
		public DateTime StopTime { get; set; }

		public string Address { get; set; }
	}
}
