using HackCovidAPICore.Model;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Threading.Tasks;

namespace HackCovidAPICore.Utilities
{
	public static class DistanceCalculator
	{
		public static List<ShopModel> GetDistance(List<ShopModel> shops, double latitude, double longitude)
		{
			try
			{
				var coord = new GeoCoordinate(latitude, longitude);
				shops.ForEach(x =>
				{
					x.Distance = Math.Round((new GeoCoordinate(x.Location.Position.Latitude, x.Location.Position.Longitude).GetDistanceTo(coord)) * 1.60934 / 1000, 2);
				});
				return shops.Where(x => x.Distance < 10).OrderBy(x => x.Distance).OrderBy(x => x.Status).ToList();
			}
			catch { }
			return null;
		}
	}
}
