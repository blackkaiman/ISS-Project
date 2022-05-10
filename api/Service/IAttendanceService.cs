using System.Collections.Generic;
using System.Threading.Tasks;
using YonderfulApi.Models;

namespace YonderfulApi.Service
{
    public interface IAttendanceService
    {
        Task<Attendance> GetAttendance(int EventId, int UserId);
        Task<IList<Attendance>> GetAllAttendance();
        Task<IList<Attendance>> GetEventsForUser(int UserId);
        Task<IList<User>> GetParticipantsForEvent(int eventId);
        Task<Attendance> CreateAttendance(Attendance newAttendance);
        Task<Attendance> UpdateAttendance(Attendance attendanceToPut);
        Task<bool> DeleteAttendance(int EventId, int UserId);

        Task<int> NumberOfParticipants(int EventId);

        Task<bool> CheckRestrictions(int EventId, Attendance newAttendance);
    }
}