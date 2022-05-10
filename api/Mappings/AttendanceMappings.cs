using AutoMapper;
using YonderfulApi.DTOs;
using YonderfulApi.Models;

namespace api.Mappings
{
    public class AttendanceMappings: Profile
    {
        public AttendanceMappings()
		{
			// source -> target
			CreateMap<Attendance, AttendanceDto>();
			CreateMap<AttendanceDto, Attendance>();
		}
    }
}