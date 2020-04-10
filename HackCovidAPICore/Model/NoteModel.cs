﻿using Microsoft.Azure.Cosmos.Spatial;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackCovidAPICore.Model
{
	public class NoteModel
	{
		[JsonProperty(PropertyName = "_self")]
		public string SelfLink { get; set; }

		[JsonProperty(PropertyName = "id")]
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
		public string Category { get; set; }

		[JsonProperty(PropertyName = "SubCategory")]
		public string SubCategory { get; set; }

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

	public class Shop
	{
		[JsonProperty(PropertyName = "ShopId")]
		public string ShopId { get; set; }

		[JsonProperty(PropertyName = "ShopEmail")]
		public string ShopEmail { get; set; }

		[JsonProperty(PropertyName = "ResponseTime")]
		public DateTime ResponseTime { get; set; }

		[JsonProperty(PropertyName = "Accepted")]
		public bool Accepted { get; set; }
	}
}