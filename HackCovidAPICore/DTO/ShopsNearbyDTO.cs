using System.ComponentModel.DataAnnotations;

namespace HackCovidAPICore.DTO
{
	public class ShopsNearbyDTO
	{
		public double Longitude { get; set; }
		public double Latitude { get; set; }

		[Required]
		public int TypeOfBusiness { get; set; }
	}
}
