using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackCovidAPICore.Enumerators
{
	public static class BusinessTypesEnum
	{
		public static List<BusinessType> BusinessTypes = new List<BusinessType>() {
			new BusinessType() { Description = "Medical / Pharmacy", TypeId = 1 },
			new BusinessType() { Description = "Groceries / Provision Stores", TypeId = 2 },
			new BusinessType() { Description = "Govt. Covid Help Centers / Hospitals", TypeId = 3 },
			new BusinessType() { Description = "Vegetables / Fruits", TypeId = 4 },
			new BusinessType() { Description = "Petrol Pumps", TypeId = 5 },
			new BusinessType() { Description = "Meat / Diary Products", TypeId = 6 }
		};
	}

	public class BusinessType
	{

		[JsonProperty(PropertyName = "TypeId")]
		public int TypeId { get; set; }

		[JsonProperty(PropertyName = "Description")]
		public string Description { get; set; }
	}


}
