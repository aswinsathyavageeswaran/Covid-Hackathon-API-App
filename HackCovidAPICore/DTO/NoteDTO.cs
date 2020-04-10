using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.Cosmos.Spatial;

namespace HackCovidAPICore.DTO
{
	public class NoteDTO
	{
		[Phone]
		[Required]
		public string UserPhoneNumber { get; set; }

		public List<Note> Notes { get; set; }

		[Required]
		public string Category { get; set; }

		public string SubCategory { get; set; }

		public double Longitude { get; set; }

		public double Latitude { get; set; }
	}

	public class Note
	{
		[Required]
		public string Description { get; set; }

		[Required]
		public string Quantity { get; set; }

		[Required]
		public string Metric { get; set; }
	}
}
