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
    public class TaskPresenceController: ControllerBase
    {
        private readonly ITaskPresenceService _TaskPresenceService;
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;

		public ITaskService TaskService => _taskService;

		public TaskPresenceController(ITaskPresenceService TaskPresenceService, ITaskService taskService, IMapper mapper){
            _TaskPresenceService = TaskPresenceService;
            _taskService = taskService;
            _mapper = mapper;
        }

        [HttpGet("{TaskId}, {userId}")]
        public async Task<IActionResult> GetTaskPresence(int TaskId, int userId){
            var TaskPresence = await _TaskPresenceService.GetTaskPresence(TaskId, userId);
            if(TaskPresence == null){
                return BadRequest("No attendace found");
            }
            return Ok(_mapper.Map<TaskPresenceDto>(TaskPresence));
        }

        [HttpGet("[action]/{TaskId}")]
        public async Task<IActionResult> GetParticipants(int TaskId){
            var participants = await _TaskPresenceService.GetParticipantsForTask(TaskId);
			return Ok(_mapper.Map<IList<UserDetailsDto>>(participants));
        }

        [Route("GetUserTasks")]
        public async Task<IActionResult> GetTasksForUser(int userId){
            var tasks = await _TaskPresenceService.GetTasksForUser(userId);
			return Ok(_mapper.Map<IList<TaskPresenceDto>>(tasks));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTaskPresence(){
            var TaskPresence = await _TaskPresenceService.GetAllTaskPresence();
            if(TaskPresence == null){
                return BadRequest("No Tasks for user found");
            }
            return Ok(_mapper.Map<IList<TaskPresenceDto>>(TaskPresence));
        }

        [HttpPost]
        public async Task<IActionResult> PostTaskPresence(TaskPresenceDto TaskPresenceDto){
            var newTaskPresence = _mapper.Map<TaskPresence>(TaskPresenceDto);
            Console.WriteLine("we got here");

            newTaskPresence = await _TaskPresenceService.CreateTaskPresence(newTaskPresence);
            if(newTaskPresence == null){
                return BadRequest();
            }
            return Ok(_mapper.Map<TaskPresenceDto>(newTaskPresence));
        }

        [HttpDelete("{TaskId}, {userId}")]
        public async Task<IActionResult> DeleteTaskPresence(int TaskId, int userId){
            var deletedTaskPresence = await _TaskPresenceService.DeleteTaskPresence(TaskId, userId);
			return deletedTaskPresence ? Ok() : BadRequest();
        }
        
        private async Task<Tuple<bool, string>> CheckPostValidations(TaskPresence TaskPresence){
            return new Tuple<bool, string>(true, "");
        }
    }
}