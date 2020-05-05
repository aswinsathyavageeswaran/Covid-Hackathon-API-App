using Microsoft.Azure.Cosmos.Spatial;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackCovidAPICore.ResponseModel
{
	public class NoteInfo
	{
		[JsonProperty(PropertyName = "Id")]
		public string Id { get; set; }

		[JsonProperty(PropertyName = "UserId")]
		public string UserId { get; set; }

		[JsonProperty(PropertyName = "NoteTime")]
		public DateTime NoteTime { get; set; }

		[JsonProperty(PropertyName = "Status")]
		public int Status { get; set; }

		[JsonProperty(PropertyName = "Notes")]
		public List<Note> Notes { get; set; }

		[JsonProperty(PropertyName = "Shops")]
		public List<Shop> Shops { get; set; }

		[JsonProperty(PropertyName = "Category")]
		public int Category { get; set; }

		[JsonProperty(PropertyName = "SubCategory")]
		public int SubCategory { get; set; }

		[JsonProperty(PropertyName = "Location")]
		public Point Location { get; set; }
	}

	public class Note
	{
		[JsonProperty(PropertyName = "Description")]
		public string Description { get; set; }

		[JsonProperty(PropertyName = "Quantity")]
		public string Quantity { get; set; }

		[JsonProperty(PropertyName = "Metric")]
		public string Metric { get; set; }
	}

	public class NotesInfo
	{
		[JsonProperty(PropertyName = "NoteInfo")]
		public List<NoteInfo> NoteInfo { get; set; }
	}
}
