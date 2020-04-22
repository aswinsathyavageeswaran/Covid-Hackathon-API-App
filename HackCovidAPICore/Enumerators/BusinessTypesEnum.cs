using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackCovidAPICore.Enumerators
{
	public static class BusinessTypesEnum
	{
		public static IDictionary<int, string> BusinessTypes = new Dictionary<int, string>()
		{
			{ 1, "Medical / Pharmacy"},
			{ 2, "Groceries / Provision Stores"},
			{ 3, "Govt. Covid Help Centers / Hospitals"},
			{ 4, "Vegetables / Fruits"},
			{ 5, "Petrol Pumps"},
			{ 6, "Meat / Diary Products"}
		};
	}
}
