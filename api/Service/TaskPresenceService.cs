using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YonderfulApi.Data;
using YonderfulApi.Models;
using YonderfulApi.Service;
using System.Linq;
using System;
namespace api.Service
{
	public class TaskPresenceService : ITaskPresenceService
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;
		public TaskPresenceService(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<bool> DeleteTaskPresence(int TaskId, int UserId)
		{
			Console.WriteLine("We got here 2");
			var TaskPresence = await _context.TaskPresence
									.Include(att => att.User)
									.Include(att => att.Task)
									.FirstOrDefaultAsync(att => att.TaskId == TaskId && att.UserId == UserId);
			if (TaskPresence == null)
			{
				return false;
			}
			Console.WriteLine("We got here 3");
			_context.TaskPresence.Remove(TaskPresence);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<IList<TaskPresence>> GetAllTaskPresence()
		{
			var TaskPresenceList = await _context.TaskPresence.Include(i => i.User).Include(i => i.Task).ToListAsync();
			return TaskPresenceList;
		}

		public async Task<TaskPresence> GetTaskPresence(int TaskId, int UserId)
		{
			var TaskPresence = await _context.TaskPresence
									.Include(i => i.User)
									.Include(i => i.Task)
									.FirstOrDefaultAsync(att => att.TaskId == TaskId && att.UserId == UserId);
			return TaskPresence;
		}

		public async Task<IList<User>> GetParticipantsForTask(int TaskId){
			var TaskPresence = await _context.TaskPresence
								.Where(att => att.TaskId == TaskId)
								.Include(att => att.User)
								.Select(att => att.User)
								.ToListAsync();
			return TaskPresence;
		}

		public async Task<IList<TaskPresence>> GetTasksForUser(int UserId)
		{
			var TaskPresence = await _context.TaskPresence
									.Where(att => att.UserId == UserId)
									.Include(i => i.User)
									.Include(i => i.Task)
									.ToListAsync();
			return TaskPresence;
		}

		public async Task<TaskPresence> CreateTaskPresence(TaskPresence newTaskPresence)
		{
			var existingTaskPresence = await GetTaskPresence(newTaskPresence.TaskId, newTaskPresence.UserId);
			if(existingTaskPresence != null)
				return null;
			_context.TaskPresence.Add(newTaskPresence);
			await _context.SaveChangesAsync();
			return newTaskPresence;
		}

		public async Task<TaskPresence> UpdateTaskPresence(TaskPresence updatedTaskPresence)
		{
			var existingTaskPresence = await GetTaskPresence(updatedTaskPresence.TaskId, updatedTaskPresence.UserId);
			if(existingTaskPresence == null)
				return null;
			existingTaskPresence.Task = updatedTaskPresence.Task;
			existingTaskPresence.User = updatedTaskPresence.User;
			
			_context.TaskPresence.Update(existingTaskPresence);
			await _context.SaveChangesAsync();
			return existingTaskPresence;
		}

		public async Task<int> NumberOfParticipants(int TaskId)
		{
			var members = await GetParticipantsForTask(TaskId);
			return members.Count;
		}

		public Task<bool> CheckRestrictions(int TaskId, TaskPresence newTaskPresence)
		{
			throw new System.NotImplementedException();
		}
	}
}