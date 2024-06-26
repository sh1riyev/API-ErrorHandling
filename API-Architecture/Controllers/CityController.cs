using System;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Cities;
using Service.Services.Interfaces;

namespace API_Architecture.Controllers
{
	[Route("api/[controller]/[action]")]
	public class CityController : ControllerBase
	{
		private readonly ICityService _cityService;
		private readonly IMapper _mapper;
		private readonly ICountryService _countryService;
		public CityController(ICityService cityService,
			IMapper mapper,
			ICountryService countryService)
		{
			_cityService = cityService;
			_mapper = mapper;
			_countryService = countryService;
		}


		[HttpPost]
		public async Task<IActionResult> Create([FromForm] CityCreateDto request)
		{
			try
			{
				if(!ModelState.IsValid) return BadRequest(ModelState);
				if (!await _countryService.IsExist(m => m.Id == request.CountryId)) return BadRequest("Choose correct country");
				var mappedCity = _mapper.Map<City>(request);
				await _cityService.Create(mappedCity);
				return CreatedAtAction(nameof(Create), request);
            }
			catch (Exception ex)
			{
				return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				 var cities = await _cityService.GetAll(null,"Country");
				 return Ok(_mapper.Map<List<CityDto>>(cities.OrderBy(m=>m.CreateDate)));
			}
			catch (Exception ex)
			{
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update([FromForm] CityUpdateDto request, int? id)
		{
			try
			{
				if (id is null) return BadRequest();
				if (!ModelState.IsValid) return BadRequest(ModelState);
				var city = await _cityService.GetEntity(m => m.Id == id);
				if (city is null) return NotFound("City doesnt exist");
				else if (!await _countryService.IsExist(m => m.Id == request.CountryId)) return BadRequest("Choose correct country");
				await _cityService.Update(_mapper.Map(request, city));
				return Ok(request);
				
			}
			catch (Exception ex)
			{
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> GetById(int? id)
		{
			try
			{
				if (id is null) return BadRequest();
				var city = await _cityService.GetEntity(m => m.Id == id);
				if (city is null) return NotFound("City doesnt exist");
				return Ok(_mapper.Map<CityDto>(city));
			}
			catch (Exception ex)
			{
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int? id)
		{
			try
			{
				if (id is null) return BadRequest();
				var city = await _cityService.GetEntity(m => m.Id == id);
				if (city is null) return NotFound("City doesnt exist");
				await _cityService.Delete(city);
				return Ok();
			}
			catch (Exception ex)
			{
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
		}
	}

}