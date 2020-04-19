using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HackCovidAPICore.DTO
{
	public class ConfirmNoteItemsDTO
	{
		[Required]
		[EmailAddress]
		public string UserEmail { get; set; }

		public List<Note> Notes { get; set; }

		[Required]
		public string NoteID { get; set; }
	}
}
