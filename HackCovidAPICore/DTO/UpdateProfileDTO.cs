using System;
using System.ComponentModel.DataAnnotations;

namespace HackCovidAPICore.DTO
{
	public class UpdateProfileDTO
	{
		[Required]
		[EmailAddress]
		public string UserEmail { get; set; }

		public string Password { get; set; }

		[Required]
		public string ShopName { get; set; }

		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required]
		public int TypeOfBusiness { get; set; }

		public double Longitude { get; set; }

		public double Latitude { get; set; }

		[Phone]
		public string Phone { get; set; }

		[Phone]
		public string DeliveryNumber { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime StopTime { get; set; }

		public string Address { get; set; }

		public string PhoneGuid { get; set; }
	}
}