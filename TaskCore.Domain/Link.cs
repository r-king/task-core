using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskCore.Domain
{
	[Table("Link")]
	public class Link : INotifyPropertyChanged, IEntity
	{
		public int Id { get; set; }
		public int ProjectTaskId { get; set; }
		public string Url { get; set; }
		public DateTime Created { get; set; }
		public DateTime LastModified { get; set; }

		private string name;

		public string Name
		{
			get { return name; }
			set
			{
				name = value;
				RaisePropertyChange("Name");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void RaisePropertyChange(string propertyname)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
		}
	}
}