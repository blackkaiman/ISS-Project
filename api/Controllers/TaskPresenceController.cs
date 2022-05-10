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
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public TaskPresenceController(ITaskPresenceService TaskPresenceService, ICategoryService categoryService, IMapper mapper){
            _TaskPresenceService = TaskPresenceService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet("{categoryId}, {userId}")]
        public async Task<IActionResult> GetTaskPresence(int categoryId, int userId){
            var TaskPresence = await _TaskPresenceService.GetTaskPresence(categoryId, userId);
            if(TaskPresence == null){
                return BadRequest("No attendace found");
            }
            return Ok(_mapper.Map<TaskPresenceDto>(TaskPresence));
        }

        [HttpGet("[action]/{categoryId}")]
        public async Task<IActionResult> GetParticipants(int categoryId){
            var participants = await _TaskPresenceService.GetParticipantsForTask(categoryId);
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
                return BadRequest("No categorys for user found");
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

        [HttpDelete("{categoryId}, {userId}")]
        public async Task<IActionResult> DeleteTaskPresence(int categoryId, int userId){
            var deletedTaskPresence = await _TaskPresenceService.DeleteTaskPresence(categoryId, userId);
			return deletedTaskPresence ? Ok() : BadRequest();
        }
        
        private async Task<Tuple<bool, string>> CheckPostValidations(TaskPresence TaskPresence){
            return new Tuple<bool, string>(true, "");
        }
    }
}