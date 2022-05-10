using System.Collections.Generic;
using api.DTOs;
using AutoMapper;
using YonderfulApi.DTOs;
using YonderfulApi.Models;

namespace YonderfulApi.Mappings
{
	public class EventMappings : Profile
	{

		public EventMappings()
		{
			// source -> target
			CreateMap<Event, EventDto>()
			.ForMember(dest => dest.BackgroundImage, src => src.MapFrom(src => src.BackgroundId.ToString()));
		}
	}
}