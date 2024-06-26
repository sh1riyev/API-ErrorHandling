using System;
namespace Service.DTOs.Admin.Cities
{
	public class CityUpdateDto
	{
		public string Name { get; set; }
		public int Population { get; set; }
		public int CountryId { get; set; }
	}
}

