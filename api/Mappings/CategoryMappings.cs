using AutoMapper;
using YonderfulApi.DTOs;
using YonderfulApi.Models;


namespace YonderfulApi.Mappings
{
	public class CategoryMappings : Profile
	{
		public CategoryMappings()
		{
			// source -> target
			CreateMap<Category, CategoryDto>().ForMember(dest => dest.Icon, src => src.MapFrom(src => src.IconId))
				.ForMember(dest => dest.DefaultBackground, src => src.MapFrom(src => src.DefaultBackgroundId));
		}
	}
}