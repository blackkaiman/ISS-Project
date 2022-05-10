using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YonderfulApi.DTOs;
using YonderfulApi.Models;
using AutoMapper;

namespace YonderfulApi.Mappings
{
	public class UserMappings : Profile
	{
		public UserMappings()
		{
			CreateMap<User, UserDto>();
			CreateMap<UserUpdateDto, User>();
			CreateMap<User, UserUpdateDto>();
			CreateMap<UserDetailsDto, User>();
			CreateMap<User, UserDetailsDto>();
			CreateMap<UserDto, User>();
		}
	}
}