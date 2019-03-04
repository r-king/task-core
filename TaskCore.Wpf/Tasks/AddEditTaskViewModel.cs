using System;
using System.ComponentModel;
using TaskCore.Data;
using TaskCore.Data.Interfaces;
using TaskCore.Domain;

namespace TaskCore.Wpf.Tasks
{
	public class AddEditTaskViewModel : BindableBase
	{
		private ITaskRepository _repository;

		public AddEditTaskViewModel(ITaskRepository repository)
		{
			_repository = repository;
			AddTaskCommand = new RelayCommand(SaveTask, CanSave);
			CancelTaskCommand = new RelayCommand(CancelProject);
		}

		private bool _editMode;

		public bool EditMode
		{
			get { return _editMode; }
			set { SetProperty(ref _editMode, value); }
		}

		private ProjectTask _editingTask;

		public ProjectTask EditingTask
		{
			get { return _editingTask; }
			set { SetProperty(ref _editingTask, value); }
		}

		private EditableTask task;

		public EditableTask Task
		{
			get { return task; }
			set { SetProperty(ref task, value); }
		}

		public RelayCommand AddTaskCommand { get; private set; }
		public RelayCommand CancelTaskCommand { get; private set; }

		public event Action Done = delegate { };

		public event Action<ProjectTask> TaskAdded = delegate { };

		public void SetTask(ProjectTask task)
		{
			EditingTask = task;
			if (Task != null) Task.ErrorsChanged -= RaiseCanExecuteChanged;
			Task = new EditableTask();
			Task.ErrorsChanged += RaiseCanExecuteChanged;
			CopyTask(task, Task);
		}

		private void RaiseCanExecuteChanged(object sender, DataErrorsChangedEventArgs e)
		{
			AddTaskCommand.RaiseCanExecuteChanged();
		}

		private async void SaveTask()
		{
			UpdateTask(Task, EditingTask);
			if (EditMode)
			{
				await _repository.UpdateAsync(EditingTask);
				Done();
			}
			else
			{
				TaskAdded(await _repository.Add(EditingTask));
			}
		}

		private void CopyTask(ProjectTask source, EditableTask target)
		{
			target.Id = source.Id;
			if (EditMode)
			{
				target.Name = source.Name;
				target.Description = source.Description;
			}
		}

		private void UpdateTask(EditableTask source, ProjectTask target)
		{
			target.Name = source.Name;
			target.Description = source.Description;
		}

		private void CancelProject()
		{
			Done();
		}

		private bool CanSave()
		{
			return !Task.HasErrors;
		}
	}
}