using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskCore.Domain
{
	[Table("Attachment")]
	public class Attachment : IEntity
	{
		public int Id { get; set; }
		public int ProjectTaskId { get; set; }
		public byte[] AttachmentFile { get; set; }
		public string Name { get; set; }
		public int Size { get; set; }
		public DateTime Created { get; set; }
		public DateTime LastModified { get; set; }
	}
}