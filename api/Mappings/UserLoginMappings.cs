using api.DTOs;
using AutoMapper;
using YonderfulApi.Models;

namespace api.Mappings
{
	public class UserLoginMappings : Profile
	{
		public UserLoginMappings()
		{
			CreateMap<User, UserLoginDto>();
			CreateMap<UserLoginDto, User>();
			CreateMap<User, UserLoginDetailsDto>();
		}
	}
}