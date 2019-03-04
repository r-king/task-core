using System;
using System.Collections.ObjectModel;
using TaskCore.Data;
using TaskCore.Data.Interfaces;
using TaskCore.Domain;

namespace TaskCore.Wpf.Tasks
{
	public class TaskListViewModel : BindableBase
	{
		private ITaskRepository _repository;

		public TaskListViewModel(ITaskRepository repository)
		{
			_repository = repository;
			AddTaskCommand = new RelayCommand(OnAddTask);			
		}

		private Project _selectedProject;

		public Project SelectedProject
		{
			get { return _selectedProject; }
			set
			{

				SetProperty(ref _selectedProject, value);
				if (_selectedProject != null)
				{
					LoadProjectTasks(_selectedProject.Id);
				}
			}
		}

		private ObservableCollection<ProjectTask> _projectTasks;

		public ObservableCollection<ProjectTask> ProjectTasks
		{
			get { return _projectTasks; }
			set { SetProperty(ref _projectTasks, value); }
		}

		public RelayCommand AddTaskCommand { get; private set; }

		public event Action SelectionChanged = delegate { };

		public event Action<ProjectTask> AddTaskRequested = delegate { };

		private void OnAddTask()
		{
			AddTaskRequested(new ProjectTask { ProjectId = SelectedProject.Id });
		}

		private async void LoadProjectTasks(int projectId)
		{
			ProjectTasks = new ObservableCollection<ProjectTask>(await _repository.GetProjectTasksAsync(projectId));
		}
	}
}