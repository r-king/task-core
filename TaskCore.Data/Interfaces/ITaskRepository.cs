using System.Collections.Generic;
using System.Threading.Tasks;
using TaskCore.Domain;

namespace TaskCore.Data.Interfaces
{
	public interface ITaskRepository : IBaseRepository<ProjectTask>
	{
		Task<List<ProjectTask>> GetProjectTasksAsync(int projectId);

		Task<List<ProjectTask>> GetOpenProjectTasksAsync(int projectId);

		Task<ProjectTask> GetProjectTaskByIdAsync(int projectTaskId);

		Task CloseProjectTask(int projectTaskId);
	}
}