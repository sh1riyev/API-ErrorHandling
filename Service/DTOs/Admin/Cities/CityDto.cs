using System;
namespace Service.DTOs.Admin.Cities
{
	public class CityDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Population { get; set; }
		public DateTime CreateDate { get; set; }
		public int CountryId { get; set; }
	}
}

