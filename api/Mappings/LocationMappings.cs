using api.DTOs;
using AutoMapper;
using YonderfulApi.Models;

namespace YonderfulApi.Mappings
{
	public class LocationMappings : Profile
	{
		public LocationMappings()
		{
			//source -> destination
			CreateMap<Location, LocationDto>();
			CreateMap<LocationDto, Location>();
		}

	}
}