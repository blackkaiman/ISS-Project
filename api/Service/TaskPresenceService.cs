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

		public async Task<bool> DeleteTaskPresence(int CategoryId, int UserId)
		{
			Console.WriteLine("We got here 2");
			var TaskPresence = await _context.TaskPresence
									.Include(att => att.User)
									.Include(att => att.Category)
									.FirstOrDefaultAsync(att => att.CategoryId == CategoryId && att.UserId == UserId);
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
			var TaskPresenceList = await _context.TaskPresence.Include(i => i.User).Include(i => i.Category).ToListAsync();
			return TaskPresenceList;
		}

		public async Task<TaskPresence> GetTaskPresence(int CategoryId, int UserId)
		{
			var TaskPresence = await _context.TaskPresence
									.Include(i => i.User)
									.Include(i => i.Category)
									.FirstOrDefaultAsync(att => att.CategoryId == CategoryId && att.UserId == UserId);
			return TaskPresence;
		}

		public async Task<IList<User>> GetParticipantsForTask(int CategoryId){
			var TaskPresence = await _context.TaskPresence
								.Where(att => att.CategoryId == CategoryId)
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
									.Include(i => i.Category)
									.ToListAsync();
			return TaskPresence;
		}

		public async Task<TaskPresence> CreateTaskPresence(TaskPresence newTaskPresence)
		{
			var existingTaskPresence = await GetTaskPresence(newTaskPresence.CategoryId, newTaskPresence.UserId);
			if(existingTaskPresence != null)
				return null;
			_context.TaskPresence.Add(newTaskPresence);
			await _context.SaveChangesAsync();
			return newTaskPresence;
		}

		public async Task<TaskPresence> UpdateTaskPresence(TaskPresence updatedTaskPresence)
		{
			var existingTaskPresence = await GetTaskPresence(updatedTaskPresence.CategoryId, updatedTaskPresence.UserId);
			if(existingTaskPresence == null)
				return null;
			existingTaskPresence.Category = updatedTaskPresence.Category;
			existingTaskPresence.User = updatedTaskPresence.User;
			
			_context.TaskPresence.Update(existingTaskPresence);
			await _context.SaveChangesAsync();
			return existingTaskPresence;
		}

		public async Task<int> NumberOfParticipants(int CategoryId)
		{
			var members = await GetParticipantsForTask(CategoryId);
			return members.Count;
		}

		public Task<bool> CheckRestrictions(int CategoryId, TaskPresence newTaskPresence)
		{
			throw new System.NotImplementedException();
		}
	}
}