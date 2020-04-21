using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HackCovidAPICore.DTO
{
	public class ShopOrderStatusDTO
	{
		[Required]
		public string NoteId { get; set; }

		[Required]
		[EmailAddress]
		public string ShopEmail { get; set; }

		/// <summary>
		/// 0 for New, 1 for Responded, 2 for Packed, 3 for Delivered
		/// </summary>
		public int Status { get; set; }
	}
}
