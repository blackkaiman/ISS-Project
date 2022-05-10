using System.Collections.Generic;
using System.Threading.Tasks;
using YonderfulApi.Models;

namespace YonderfulApi.Service
{
    public interface ITaskPresenceService
    {
        Task<TaskPresence> GetTaskPresence(int taskId, int UserId);
        Task<IList<TaskPresence>> GetAllTaskPresence();
        Task<IList<TaskPresence>> GetTasksForUser(int UserId);
        Task<IList<User>> GetParticipantsForTask(int taskId);
        Task<TaskPresence> CreateTaskPresence(TaskPresence newTaskPresence);
        Task<TaskPresence> UpdateTaskPresence(TaskPresence TaskPresenceToPut);
        Task<bool> DeleteTaskPresence(int taskId, int UserId);

        Task<int> NumberOfParticipants(int taskId);

        Task<bool> CheckRestrictions(int taskId, TaskPresence newTaskPresence);
    }
}