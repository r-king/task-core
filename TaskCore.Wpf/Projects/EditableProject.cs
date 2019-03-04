using System.ComponentModel.DataAnnotations;

namespace TaskCore.Wpf.Projects
{
	public class EditableProject : BindableBase
	{
		private int id;

		public int Id
		{
			get { return id; }
			set { SetProperty(ref id, value); }
		}

		private string name;

		[Required]
		public string Name
		{
			get { return name; }
			set
			{
				SetProperty(ref name, value);
			}
		}

		private string description;

		public string Description
		{
			get { return description; }
			set
			{
				SetProperty(ref description, value);
			}
		}
	}
}