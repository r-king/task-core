using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskCore.Data.Interfaces;
using TaskCore.Domain;

namespace TaskCore.Data
{
	public class ProjectRepository : BaseRepository<Project>, IProjectRepository
	{
		public Task<Project> CloseProject(Project project)
		{
			project.ProjectStatus = "C";
			return UpdateAsync(project);
		}

		public async Task<List<Project>> GetOpenProjectsAsync()
		{
			_context = new TaskCoreContext();
			return await _context.Projects.AsNoTracking().Where(p => p.ProjectStatus == "O").ToListAsync();
		}

		public IEnumerable<Project> GetProjects()
		{
			return _context.Projects;
		}

		public async Task<List<Project>> GetProjectsAsync()
		{
			return await _context.Projects.ToListAsync();
		}

		public Task<Project> ReopenProject(Project project)
		{
			project.ProjectStatus = "O";
			return UpdateAsync(project);
		}
	}
}