using System;
using System.Collections.ObjectModel;
using TaskCore.Data.Interfaces;
using TaskCore.Domain;

namespace TaskCore.Wpf.Projects
{
	public class ProjectListViewModel : BindableBase
	{
		private IProjectRepository _repository;

		public ProjectListViewModel(IProjectRepository repository)
		{
			_repository = repository;
			AddProjectCommand = new RelayCommand(OnAddProject);
			EditProjectCommand = new RelayCommand<Project>(EditProject);
			CloseProjectCommand = new RelayCommand<Project>(CloseProject);
			ReopenProjectCommand = new RelayCommand<Project>(ReopenProject);
			DeleteProjectCommand = new RelayCommand<Project>(DeleteProject);
			ShowAllProjectsCommand = new RelayCommand<Project>(ShowAllProjects);

			ProjectActions = new ObservableCollection<ContextAction<Project>>
			{
				new ContextAction<Project>
				{
					Name = "Edit Project",
					Action = EditProjectCommand
				},

				new ContextAction<Project>
				{
					Name = "Close Project",
					Action = CloseProjectCommand
				},

				new ContextAction<Project>
				{
					Name = "Delete Project",
					Action = DeleteProjectCommand
				}
			};
		}

		public RelayCommand AddProjectCommand { get; private set; }
		public RelayCommand<Project> EditProjectCommand { get; set; }
		public RelayCommand<Project> CloseProjectCommand { get; set; }
		public RelayCommand<Project> ReopenProjectCommand { get; set; }
		public RelayCommand<Project> DeleteProjectCommand { get; set; }
		public RelayCommand<Project> ShowAllProjectsCommand { get; set; }

		public event Action<Project> AddProjectRequested = delegate { };

		public event Action<Project> EditProjectRequested = delegate { };

		private Project _selectedProject;

		public Project SelectedProject
		{
			get { return _selectedProject; }
			set { SetProperty(ref _selectedProject, value); }
		}

		private ObservableCollection<Project> _projects;

		public ObservableCollection<Project> Projects
		{
			get
			{
				return _projects;
			}
			set
			{
				SetProperty(ref _projects, value);
			}
		}

		private ObservableCollection<ContextAction<Project>> projectActions;

		public ObservableCollection<ContextAction<Project>> ProjectActions
		{
			get { return projectActions; }
			set { SetProperty(ref projectActions, value); }
		}

		private void OnAddProject()
		{
			AddProjectRequested(new Project());
		}

		public async void LoadProjects()
		{
			Projects = new ObservableCollection<Project>(await _repository.GetOpenProjectsAsync());
		}

		private async void ShowAllProjects(Project obj)
		{
			Projects = new ObservableCollection<Project>(await _repository.GetProjectsAsync());
		}

		private async void DeleteProject(Project obj)
		{
			await _repository.Delete(obj.Id);
			LoadProjects();
		}

		private async void ReopenProject(Project obj)
		{
			await _repository.ReopenProject(obj);
			LoadProjects();
		}

		private async void CloseProject(Project obj)
		{
			await _repository.CloseProject(obj);
			LoadProjects();
		}

		private void EditProject(Project obj)
		{
			EditProjectRequested(obj);
		}
	}
}