using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskCore.Domain
{
	[Table("Comment")]
	public class Comment : IEntity
	{
		public int Id { get; set; }
		public int ProjectTaskId { get; set; }
		public string Text { get; set; }
		public DateTime Created { get; set; }
		public DateTime LastModified { get; set; }
	}
}