using System.ComponentModel.DataAnnotations;

namespace HackCovidAPICore.DTO
{
	public class ShopStatusDTO
	{
		[Required]
		[EmailAddress]
		public string UserEmail { get; set; }

		[Required]
		public int Status { get; set; }
	}
}
