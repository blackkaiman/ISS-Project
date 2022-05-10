using AutoMapper;
using YonderfulApi.DTOs;
using YonderfulApi.Models;


namespace YonderfulApi.Mappings
{
	public class TaskMappings : Profile
	{
		public TaskMappings()
		{
			// source -> target
			CreateMap<TaskEmp, TaskEmpDto>();
			CreateMap<TaskEmpDto, TaskEmp>();
		}
	}
}