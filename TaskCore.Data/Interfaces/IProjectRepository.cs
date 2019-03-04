using System.Collections.Generic;
using System.Threading.Tasks;
using TaskCore.Domain;

namespace TaskCore.Data.Interfaces
{
	public interface IProjectRepository : IBaseRepository<Project>
	{
		IEnumerable<Project> GetProjects();

		Task<List<Project>> GetProjectsAsync();

		Task<Project> CloseProject(Project project);

		Task<Project> ReopenProject(Project project);

		Task<List<Project>> GetOpenProjectsAsync();

	}
}