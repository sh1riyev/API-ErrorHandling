using System;
using FluentValidation;

namespace Service.DTOs.Admin.Cities
{
	public class CityCreateDto
	{
		public string Name { get; set; }
		public int CountryId { get; set; }
		public int Population { get; set; }
	}

	public class CityCreateDtoValidatior : AbstractValidator<CityCreateDto>
	{
		public CityCreateDtoValidatior()
		{
			RuleFor(m => m.Name).NotNull().WithMessage("Name is required");
            RuleFor(m => m.CountryId).NotNull().WithMessage("Must assing Country");
            RuleFor(m => m.Population).NotNull().WithMessage("Population cannot be 0");

        }
    }
}

