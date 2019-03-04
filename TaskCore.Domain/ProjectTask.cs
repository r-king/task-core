using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;

namespace TaskCore.Domain
{
	[Table("ProjectTask")]
	public class ProjectTask : INotifyPropertyChanged, IEntity
	{
		public int Id { get; set; }
		public int ProjectId { get; set; }

		private string name;

		public string Name
		{
			get { return name; }
			set
			{
				name = value;
				NotifyPropertyChanged();
			}
		}

		private string description;

		public string Description
		{
			get { return description; }
			set
			{
				description = value;
				NotifyPropertyChanged();
			}
		}

		public List<SubTask> SubTasks { get; set; }
		public List<Comment> Comments { get; set; }
		public List<Attachment> Attachments { get; set; }
		//public List<Link> Links { get; set; }

		private List<Link> links;

		public List<Link> Links
		{
			get { return links; }
			set
			{
				links = value;
				NotifyPropertyChanged();
			}
		}

		public string TaskStatus { get; set; }
		public DateTime Created { get; set; }
		public DateTime LastModified { get; set; }

		public int CompletedSubTasks
		{
			get
			{
				return SubTasks.Count(x => x.IsComplete == true);
			}
			private set { }
		}

		public event PropertyChangedEventHandler PropertyChanged;

		// This method is called by the Set accessor of each property.
		// The CallerMemberName attribute that is applied to the optional propertyName
		// parameter causes the property name of the caller to be substituted as an argument.
		private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}