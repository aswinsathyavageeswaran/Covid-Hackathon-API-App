using System.ComponentModel.DataAnnotations;

namespace HackCovidAPICore.DTO
{
	public class UserLoginDTO
	{
		[Required]
		[EmailAddress]
		public string UserEmail { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
