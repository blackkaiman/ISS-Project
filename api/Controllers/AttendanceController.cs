using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YonderfulApi.DTOs;
using YonderfulApi.Models;
using YonderfulApi.Service;

namespace YonderfulApi.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
    public class AttendanceController: ControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;

        public AttendanceController(IAttendanceService attendanceService, IEventService eventService, IMapper mapper){
            _attendanceService = attendanceService;
            _eventService = eventService;
            _mapper = mapper;
        }

        [HttpGet("{eventId}, {userId}")]
        public async Task<IActionResult> GetAttendance(int eventId, int userId){
            var attendance = await _attendanceService.GetAttendance(eventId, userId);
            if(attendance == null){
                return BadRequest("No attendace found");
            }
            return Ok(_mapper.Map<AttendanceDto>(attendance));
        }

        [HttpGet("[action]/{eventId}")]
        public async Task<IActionResult> GetParticipants(int eventId){
            var participants = await _attendanceService.GetParticipantsForEvent(eventId);
			return Ok(_mapper.Map<IList<UserDetailsDto>>(participants));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAttendance(){
            var attendance = await _attendanceService.GetAllAttendance();
            if(attendance == null){
                return BadRequest("No events for user found");
            }
            return Ok(_mapper.Map<IList<AttendanceDto>>(attendance));
        }

        [HttpPost]
        public async Task<IActionResult> PostAttendance(AttendanceDto attendanceDto){
            var newAttendance = _mapper.Map<Attendance>(attendanceDto);
            newAttendance.JoiningDate = DateTime.UtcNow.ToLocalTime();
            
            var isValid = await CheckPostValidations(newAttendance);

            if(isValid.Item1 == false){
                return BadRequest(isValid.Item2);
            }

            newAttendance = await _attendanceService.CreateAttendance(newAttendance);
            if(newAttendance == null){
                return BadRequest();
            }
            return Ok(_mapper.Map<AttendanceDto>(newAttendance));
        }

        [HttpDelete("{eventId}, {userId}")]
        public async Task<IActionResult> DeleteAttendance(int eventId, int userId){
            var deletedAttendance = await _attendanceService.DeleteAttendance(eventId, userId);
			return deletedAttendance ? Ok() : BadRequest();
        }
        
        private async Task<Tuple<bool, string>> CheckPostValidations(Attendance attendance){
            var attendanceEvent = await _eventService.GetEvent(attendance.EventId);
            var maxMembers = attendanceEvent.MaximumParticipants;
            var numberOfMembers = await _attendanceService.NumberOfParticipants(attendance.EventId);
            var joiningDeadline = attendanceEvent.JoinDeadline;

            if(numberOfMembers >= maxMembers && maxMembers != 0){
                return new Tuple<bool, string>(false, "The maximum capacity of the event is reached");
            }
            
            if(joiningDeadline < attendance.JoiningDate){
                return new Tuple<bool, string>(false, "The joining deadline has passed");
            }
            return new Tuple<bool, string>(true, "");
        }
    }
}