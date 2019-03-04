using System;
using TaskCore.Data;
using TaskCore.Data.Interfaces;
using TaskCore.Domain;

namespace TaskCore.Wpf.SubTasks
{
	public class AddEditSubTaskViewModel : BindableBase
	{
		public ISubTaskRepository subTaskRepository;

		public AddEditSubTaskViewModel(ISubTaskRepository subTaskRepository)
		{
			this.subTaskRepository = subTaskRepository;

			CancelCommand = new RelayCommand(Close);
			AddSubTaskCommand = new RelayCommand(AddSubTask);
		}

		private async void AddSubTask()
		{
			string[] subTasks = SubTaskText.Split(
				new[] { "\r\n", "\r", "\n" }, 
				StringSplitOptions.None);

			foreach (string item in subTasks)
			{
				await subTaskRepository.Add(new SubTask
				{
					ProjectTaskId = ProjectTaskId,
					Name = item,
					IsComplete = false
				});
			}
			subTaskText = null;
			Close();
		}

		private void Close()
		{
			Done();
		}

		private string subTaskText;

		public string SubTaskText

		{
			get { return subTaskText; }
			set { SetProperty(ref subTaskText, value); }
		}

		private int projectTaskId;

		public int ProjectTaskId
		{
			get { return projectTaskId; }
			set { SetProperty(ref projectTaskId, value); }
		}

		public RelayCommand CancelCommand { get; set; }
		public RelayCommand AddSubTaskCommand { get; set; }

		public event Action Done = delegate { };
	}
}