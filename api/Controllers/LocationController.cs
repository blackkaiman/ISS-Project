using System.Collections.Generic;
using System.Threading.Tasks;
using api.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YonderfulApi.Service;

namespace YonderfulApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LocationController : ControllerBase
	{
		private readonly ILocationService _locationService;
		private readonly IMapper _mapper;

		public LocationController(ILocationService locationService, IMapper mapper)
		{
			_locationService = locationService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetLocations()
		{
			var myLocationList = await _locationService.GetLocationList();
			return Ok(_mapper.Map<IList<LocationDto>>(myLocationList));
		}

		[HttpDelete("{locationId}")]
		public async Task<IActionResult> DeleteLocation(int locationId)
		{
			var deletedLocation = await _locationService.DeleteLocation(locationId);
			return deletedLocation ? Ok() : BadRequest();
		}

	}
}