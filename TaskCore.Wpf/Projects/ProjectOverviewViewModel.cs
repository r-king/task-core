using System.Diagnostics;
using TaskCore.Domain;
using TaskCore.Wpf.Tasks;
using Unity;

namespace TaskCore.Wpf.Projects
{
	public class ProjectOverviewViewModel : BindableBase
	{
		public ProjectOverviewViewModel()
		{
			Debug.WriteLine("ProjectOverviewViewModel");

			ProjectListViewModel = ContainerHelper.Container.Resolve<ProjectListViewModel>();
			TaskListViewModel = ContainerHelper.Container.Resolve<TaskListViewModel>();
			AddEditProjectViewModel = ContainerHelper.Container.Resolve<AddEditProjectViewModel>();
			AddEditTaskViewModel = ContainerHelper.Container.Resolve<AddEditTaskViewModel>();

			PopUpViewModel = new PopUpViewModel();

			ProjectListViewModel.AddProjectRequested += ShowAddProject;
			ProjectListViewModel.EditProjectRequested += ShowEditProject;
			taskListViewModel.AddTaskRequested += ShowAddProjectTask;

			AddEditProjectViewModel.Done += AddEditProjectClose;

			AddEditTaskViewModel.TaskAdded += OnTaskAddedDone;
			AddEditTaskViewModel.Done += ClosePopUpView;
		}

		private void AddEditProjectClose()
		{
			projectListViewModel.LoadProjects();
			ClosePopUpView();
		}

		private void ShowEditProject(Project obj)
		{
			AddEditProjectViewModel.EditMode = true;
			AddEditProjectViewModel.SetProject(obj);
			PopUpViewModel.CurrentViewModel = AddEditProjectViewModel;
			IsPopUpVisible = true;
		}

		private void OnTaskAddedDone(ProjectTask projectTask)
		{
			PopUpViewModel.CurrentViewModel = null;
			TaskListViewModel.ProjectTasks.Add(projectTask);
			IsPopUpVisible = false;
		}

		private void ClosePopUpView()
		{
			PopUpViewModel.CurrentViewModel = null;
			IsPopUpVisible = false;
		}

		private void OnProjectAddedDone(Project project)
		{
			PopUpViewModel.CurrentViewModel = null;
			projectListViewModel.LoadProjects();
			IsPopUpVisible = false;
		}

		private void OnProjectUpdatedDone(Project obj)
		{
			PopUpViewModel.CurrentViewModel = null;
			projectListViewModel.LoadProjects();
			IsPopUpVisible = false;
		}

		private void ShowAddProject(Project project)
		{
			AddEditProjectViewModel.SetProject(project);
			PopUpViewModel.CurrentViewModel = AddEditProjectViewModel;
			IsPopUpVisible = true;
		}

		private void ShowAddProjectTask(ProjectTask projectTask)
		{
			AddEditTaskViewModel.SetTask(projectTask);
			PopUpViewModel.CurrentViewModel = AddEditTaskViewModel;
			IsPopUpVisible = true;
		}

		private ProjectListViewModel projectListViewModel;

		public ProjectListViewModel ProjectListViewModel
		{
			get { return projectListViewModel; }
			set { SetProperty(ref projectListViewModel, value); }
		}

		private TaskListViewModel taskListViewModel;

		public TaskListViewModel TaskListViewModel
		{
			get { return taskListViewModel; }
			set { SetProperty(ref taskListViewModel, value); }
		}

		private PopUpViewModel popUpViewModel;

		public PopUpViewModel PopUpViewModel
		{
			get { return popUpViewModel; }
			set { SetProperty(ref popUpViewModel, value); }
		}

		private bool isPopUpVisible;

		public bool IsPopUpVisible
		{
			get { return isPopUpVisible; }
			set { SetProperty(ref isPopUpVisible, value); }
		}

		private AddEditProjectViewModel addEditProjectViewModel;

		public AddEditProjectViewModel AddEditProjectViewModel
		{
			get { return addEditProjectViewModel; }
			set { SetProperty(ref addEditProjectViewModel, value); }
		}

		private AddEditTaskViewModel addEditTaskViewModel;

		public AddEditTaskViewModel AddEditTaskViewModel
		{
			get { return addEditTaskViewModel; }
			set { SetProperty(ref addEditTaskViewModel, value); }
		}
	}
}