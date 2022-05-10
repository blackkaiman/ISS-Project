using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YonderfulApi.DTOs;
using YonderfulApi.Service;
using YonderfulApi.Models;
using System;

namespace YonderfulApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TaskController : ControllerBase
	{
		private readonly ITaskService _taskService;
		private readonly IMapper _mapper;

		public TaskController(ITaskService taskService, IMapper mapper)
		{
			_taskService = taskService;
			_mapper = mapper;
		}

		[HttpGet("{TaskId}")]
		public async Task<IActionResult> GetTask(int TaskId)
		{
			var Task = await _taskService.GetTask(TaskId);
			if (Task == null)
			{
				return NotFound();
			}
			var TaskDto =(_mapper.Map<TaskEmpDto>(Task));
			return Ok(TaskDto);
		}

		[HttpGet]
		public async Task<IActionResult> GetTaskList()
		{
			var TaskList = await _taskService.GetTaskList();
			if (TaskList == null)
			{
				return NotFound();
			}
			var TaskDtoList = _mapper.Map<IList<TaskEmpDto>>(TaskList);
			return Ok(TaskDtoList);
		}

		[HttpPost]
		public async Task<IActionResult> PostTask(TaskEmpDto Task)
		{
			TaskEmp newTask = await _taskService.CreateTask(Task);

			var createdTask = await _taskService.PostTask(newTask);
			if (createdTask == null)
			{
				return BadRequest("Task already exists");
			}
			return Created(nameof(GetTask), _mapper.Map<TaskEmpDto>(createdTask));
		}

		[HttpDelete]
		public async Task<ActionResult> DeleteTask(int TaskId)
		{
			var removedTask = await _taskService.DeleteTask(TaskId);
			return removedTask ? Ok() : BadRequest();
		}

		[HttpPut]
		public async Task<ActionResult> PutTask(TaskEmpDto updatedTask)
		{
			TaskEmp TaskToPut = await _taskService.CreateTask(updatedTask);

			var newTask = await _taskService.PutTask(updatedTask.Id, TaskToPut);
			if (newTask == null)
			{
				return BadRequest();
			}
			return Created(nameof(GetTask), _mapper.Map<TaskEmpDto>(newTask));
		}
	}
}