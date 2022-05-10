using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YonderfulApi.Data;
using YonderfulApi.Models;
using YonderfulApi.Service;
using System.Linq;

namespace api.Service
{
	public class AttendanceService : IAttendanceService
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;
		public AttendanceService(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<bool> DeleteAttendance(int EventId, int UserId)
		{
			var attendance = await _context.Attendance
									.Include(att => att.User)
									.Include(att => att.Event)
									.FirstOrDefaultAsync(att => att.EventId == EventId && att.UserId == UserId);
			if (attendance == null)
			{
				return false;
			}
			_context.Attendance.Remove(attendance);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<IList<Attendance>> GetAllAttendance()
		{
			var attendanceList = await _context.Attendance.Include(i => i.User).Include(i => i.Event).ToListAsync();
			return attendanceList;
		}

		public async Task<Attendance> GetAttendance(int EventId, int UserId)
		{
			var attendance = await _context.Attendance
									.Include(i => i.User)
									.Include(i => i.Event)
									.FirstOrDefaultAsync(att => att.EventId == EventId && att.UserId == UserId);
			return attendance;
		}

		public async Task<IList<User>> GetParticipantsForEvent(int eventId){
			var attendance = await _context.Attendance
								.Where(att => att.EventId == eventId)
								.Include(att => att.User)
								.Select(att => att.User)
								.ToListAsync();
			return attendance;
		}

		public async Task<IList<Attendance>> GetEventsForUser(int UserId)
		{
			var attendance = await _context.Attendance
									.Where(att => att.UserId == UserId)
									.Include(i => i.User)
									.Include(i => i.Event)
									.ToListAsync();
			return attendance;
		}

		public async Task<Attendance> CreateAttendance(Attendance newAttendance)
		{
			var existingAttendance = await GetAttendance(newAttendance.EventId, newAttendance.UserId);
			if(existingAttendance != null)
				return null;
			_context.Attendance.Add(newAttendance);
			await _context.SaveChangesAsync();
			return newAttendance;
		}

		public async Task<Attendance> UpdateAttendance(Attendance updatedAttendance)
		{
			var existingAttendance = await GetAttendance(updatedAttendance.EventId, updatedAttendance.UserId);
			if(existingAttendance == null)
				return null;
			existingAttendance.Event = updatedAttendance.Event;
			existingAttendance.User = updatedAttendance.User;
			existingAttendance.JoiningDate = updatedAttendance.JoiningDate;
			_context.Attendance.Update(existingAttendance);
			await _context.SaveChangesAsync();
			return existingAttendance;
		}

		public async Task<int> NumberOfParticipants(int EventId)
		{
			var members = await GetParticipantsForEvent(EventId);
			return members.Count;
		}

		public Task<bool> CheckRestrictions(int EventId, Attendance newAttendance)
		{
			throw new System.NotImplementedException();
		}
	}
}