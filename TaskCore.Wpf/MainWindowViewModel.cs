using TaskCore.Domain;
using TaskCore.Wpf.Projects;
using TaskCore.Wpf.Tasks;
using Unity;

namespace TaskCore.Wpf
{
	public class MainWindowViewModel : BindableBase
	{
		private BindableBase currentViewModel;

		private ProjectOverviewViewModel projectOverviewViewModel;

		private TaskDetailViewModel taskDetailViewModel;

		public MainWindowViewModel()
		{
			OpenTaskDetail = new RelayCommand<ProjectTask>(TaskDeatil);
			OpenProjectOverview = new RelayCommand(ProjectOverview);
			TaskDetailViewModel = ContainerHelper.Container.Resolve<TaskDetailViewModel>();
			ProjectOverviewViewModel = new ProjectOverviewViewModel();

			TaskDetailViewModel.Close += CloseTaskDetail;

			CurrentViewModel = ProjectOverviewViewModel;
		}

		public BindableBase CurrentViewModel
		{
			get { return currentViewModel; }
			set { SetProperty(ref currentViewModel, value); }
		}

		public RelayCommand OpenProjectOverview { get; private set; }

		public RelayCommand<ProjectTask> OpenTaskDetail { get; private set; }

		public ProjectOverviewViewModel ProjectOverviewViewModel
		{
			get { return projectOverviewViewModel; }
			set { SetProperty(ref projectOverviewViewModel, value); }
		}

		public TaskDetailViewModel TaskDetailViewModel
		{
			get { return taskDetailViewModel; }
			set { SetProperty(ref taskDetailViewModel, value); }
		}

		private void CloseTaskDetail()
		{
			CurrentViewModel = projectOverviewViewModel;
		}

		private void ProjectOverview()
		{
			CurrentViewModel = ProjectOverviewViewModel;
		}

		private void TaskDeatil(ProjectTask projectTask)
		{
			TaskDetailViewModel.ProjectTask = projectTask;
			CurrentViewModel = TaskDetailViewModel;
		}
	}
}