using System.Collections.Generic;
using System.Threading.Tasks;
using YonderfulApi.Models;
using YonderfulApi.Data;
using Microsoft.EntityFrameworkCore;
using YonderfulApi.DTOs;
using Microsoft.AspNetCore.Components;
using System;
namespace YonderfulApi.Service
{
	public class TaskService : ITaskService
	{
		private readonly DataContext _context;
	
		public TaskService(DataContext context)
		{
			_context = context;
		}

		public async Task<TaskEmp> GetTask(int TaskId)
		{
			var Task = await _context.Tasks.FindAsync(TaskId);
			return Task;
		}

		public async Task<IList<TaskEmp>> GetTaskList()
		{
			var TaskList = await _context.Tasks.ToListAsync();
			return TaskList;
		}

		public async Task<TaskEmp> PostTask(TaskEmp newTask)
		{
			if (await TaskExists(newTask)) return null;

			_context.Tasks.Add(newTask);
			await _context.SaveChangesAsync();
			return newTask;
		}

		public async Task<bool> DeleteTask(int TaskId)
		{
			var Task = await _context.Tasks.FindAsync(TaskId);
			if (Task == null)
			{
				return false;
			}
			_context.Tasks.Remove(Task);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<TaskEmp> PutTask(int TaskId, TaskEmp TaskToPut)
		{
			var Task = await _context.Tasks.FindAsync(TaskId);
			if (Task == null)
			{
				return null;
			}
			Task.Title = TaskToPut.Title;

			await _context.SaveChangesAsync();
			return Task;
		}
		private async Task<bool> TaskExists(TaskEmp Task)
		{
			return await _context.Tasks.AnyAsync(cat => cat.Title.ToLower() == Task.Title.ToLower());
		}


		public async Task<TaskEmp> CreateTask(TaskEmpDto TaskDto)
		{
			TaskEmp newTask = new TaskEmp
			{
				Title = TaskDto.Title,
			};
			return newTask;
		}
	}
}