using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HackCovidAPICore.DTO
{
	public class ConfirmOrderDTO
	{
		[Required]
		public string NoteId { get; set; }

		[Required][EmailAddress]
		public string ShopEmail { get; set; }
	}
}
