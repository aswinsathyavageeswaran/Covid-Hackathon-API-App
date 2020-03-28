using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
