using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskCore.Data.Interfaces;
using TaskCore.Domain;

namespace TaskCore.Data
{
	public class TaskRepository : BaseRepository<ProjectTask>, ITaskRepository
	{
		public async Task<List<ProjectTask>> GetProjectTasksAsync(int projectId)
		{
			return await _context.Tasks.Where(e => e.ProjectId == projectId)
				.Include(x => x.SubTasks)
				.Include(x => x.Attachments)
				.Include(x => x.Links)
				.Include(x => x.Comments)
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<ProjectTask> GetProjectTaskByIdAsync(int projetcTaskId)
		{
			return await _context.Tasks.Where(e => e.Id == projetcTaskId)
				.Include(x => x.SubTasks)
				.Include(x => x.Attachments)
				.Include(x => x.Links)
				.Include(x => x.Comments)
				.AsNoTracking()
				.FirstOrDefaultAsync();
		}

		public async Task CloseProjectTask(int projectTaskId)
		{
			ProjectTask projectTask = _context.Tasks.Where(p => p.Id == projectTaskId).FirstOrDefault();
			projectTask.TaskStatus = "C";
			await UpdateAsync(projectTask);
		}

		public async Task<List<ProjectTask>> GetOpenProjectTasksAsync(int projectId)
		{
			return await _context.Tasks.Where(e => e.ProjectId == projectId)
				.Where(p => p.TaskStatus == "O")
				.Include(x => x.SubTasks)
				.Include(x => x.Attachments)
				.Include(x => x.Links)
				.Include(x => x.Comments)
				.AsNoTracking()
				.ToListAsync();
		}
	}
}