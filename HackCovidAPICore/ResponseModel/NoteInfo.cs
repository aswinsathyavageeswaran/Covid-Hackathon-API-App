using Microsoft.Azure.Cosmos.Spatial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackCovidAPICore.ResponseModel
{
	public class NoteInfo
	{
		public string Id { get; set; }
		public string UserId { get; set; }
		public DateTime NoteTime { get; set; }
		public int Status { get; set; }
		public List<Note> Notes { get; set; }
		public List<Shop> Shops { get; set; }
		public int Category { get; set; }
		public int SubCategory { get; set; }
		public Point Location { get; set; }
	}

	public class Note
	{
		public string Description { get; set; }
		public string Quantity { get; set; }
		public string Metric { get; set; }
	}

	public class NotesInfo
	{
		public List<NoteInfo> NoteInfo { get; set; }
	}
}
