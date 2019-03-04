using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskCore.Domain
{
	[Table("Project")]
	public class Project : IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public IEnumerable<ProjectTask> ProjectTasks { get; set; }
		public DateTime Created { get; set; }
		public DateTime LastModified { get; set; }
		public string ProjectStatus { get; set; }
	}
}