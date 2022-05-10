using AutoMapper;
using YonderfulApi.DTOs;
using YonderfulApi.Models;

namespace api.Mappings
{
    public class TaskPresenceMappings: Profile
    {
        public TaskPresenceMappings()
		{
			// source -> target
			CreateMap<TaskPresence, TaskPresenceDto>();
			CreateMap<TaskPresenceDto, TaskPresence>();
		}
    }
}