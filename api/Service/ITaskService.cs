using System.Collections.Generic;
using System.Threading.Tasks;
using YonderfulApi.DTOs;
using YonderfulApi.Models;

namespace YonderfulApi.Service
{
	public interface ITaskService
	{
		Task<IList<TaskEmp>> GetTaskList();
		Task<TaskEmp> GetTask(int TaskId);
		Task<TaskEmp> PostTask(TaskEmp newTask);

		Task<TaskEmp> PutTask(int TaskId, TaskEmp TaskToPut);

		Task<bool> DeleteTask(int TaskId);

		Task<TaskEmp> CreateTask(TaskEmpDto TaskDto);
	}
}