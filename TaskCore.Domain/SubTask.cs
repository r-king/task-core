using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskCore.Domain
{
	[Table("SubTask")]
	public class SubTask : IEntity
	{
		public int Id { get; set; }
		public int ProjectTaskId { get; set; }
		[Required]
		public string Name { get; set; }
		public bool IsComplete { get; set; }
		public DateTime Created { get; set; }
		public DateTime LastModified { get; set; }
	}
}