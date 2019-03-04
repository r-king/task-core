using System.Collections.ObjectModel;
using TaskCore.Data;
using TaskCore.Data.Interfaces;
using TaskCore.Domain;

namespace TaskCore.Wpf.SubTasks
{
	public class SubTaskListViewModel : BindableBase
	{
		public ISubTaskRepository subTaskRepository { get; set; }

		public SubTaskListViewModel(ISubTaskRepository subTaskRepository)
		{
			this.subTaskRepository = subTaskRepository;
			DeleteSubTaskCommand = new RelayCommand<SubTask>(DeleteSubTask);
			MarkAsCompleteCommand = new RelayCommand<SubTask>(MarkAsComplete);
		}

		private ObservableCollection<SubTask> subTasks;

		public ObservableCollection<SubTask> SubTasks
		{
			get { return subTasks; }
			set { SetProperty(ref subTasks, value); }
		}

		public RelayCommand<SubTask> DeleteSubTaskCommand { get; set; }
		public RelayCommand<SubTask> MarkAsCompleteCommand { get; set; }

		private async void MarkAsComplete(SubTask obj)
		{
			obj.IsComplete = !obj.IsComplete;
			await subTaskRepository.UpdateAsync(obj);
		}

		private void DeleteSubTask(SubTask obj)
		{
			SubTasks.Remove(obj);
			subTaskRepository.Delete(obj.Id);
		}
	}
}