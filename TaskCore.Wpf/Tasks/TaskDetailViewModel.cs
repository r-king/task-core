using System;
using System.Collections.ObjectModel;
using System.Windows;
using TaskCore.Data;
using TaskCore.Data.Interfaces;
using TaskCore.Domain;
using TaskCore.Wpf.Attachments;
using TaskCore.Wpf.Comments;
using TaskCore.Wpf.Links;
using TaskCore.Wpf.SubTasks;
using Unity;

namespace TaskCore.Wpf.Tasks
{
	public class TaskDetailViewModel : BindableBase
	{
		private ITaskRepository taskRepository;

		public TaskDetailViewModel(ITaskRepository taskRepository)
		{
			this.taskRepository = taskRepository;

			AddEditTaskViewModel = ContainerHelper.Container.Resolve<AddEditTaskViewModel>();
			AddEditAttachmentViewModel = ContainerHelper.Container.Resolve<AddEditAttachmentViewModel>();
			AddEditCommentViewModel = ContainerHelper.Container.Resolve<AddEditCommentViewModel>();
			AddEditLinkViewModel = ContainerHelper.Container.Resolve<AddEditLinkViewModel>();
			AddEditSubTaskViewModel = ContainerHelper.Container.Resolve<AddEditSubTaskViewModel>();
			AttachmentListViewModel = ContainerHelper.Container.Resolve<AttachmentListViewModel>();
			CommentListViewModel = ContainerHelper.Container.Resolve<CommentListViewModel>();
			LinkListViewModel = ContainerHelper.Container.Resolve<LinkListViewModel>();
			SubTaskListViewModel = ContainerHelper.Container.Resolve<SubTaskListViewModel>();

			EditTaskCommand = new RelayCommand(OnEditTask);
			ClosePopUpCommand = new RelayCommand(ClosePopUpView);
			AddAttachmentCommand = new RelayCommand(AddAttachment);
			AddCommentCommand = new RelayCommand(AddComment);
			AddLinkCommand = new RelayCommand(AddLink);
			AddSubTaskCommand = new RelayCommand(AddSubTask);
			DeleteTaskCommand = new RelayCommand(DeleteTask);
			ExpandAllCommand = new RelayCommand(ExpandAll);
			CloseTaskCommand = new RelayCommand(CloseTask);

			AddEditAttachmentViewModel.Done += ClosePopUpView;
			AddEditTaskViewModel.Done += ClosePopUpView;
			AddEditCommentViewModel.Done += ClosePopUpView;
			AddEditLinkViewModel.Done += ClosePopUpView;
			AddEditSubTaskViewModel.Done += ClosePopUpView;
			AddEditTaskViewModel.TaskAdded += TaskAdded;

			PopUpViewModel = new PopUpViewModel();
		}		

		private ProjectTask projectTask;

		public ProjectTask ProjectTask
		{
			get { return projectTask; }
			set
			{
				SetProperty(ref projectTask, value);
				AttachmentListViewModel.Attachments = ProjectTask.Attachments == null 
					? new ObservableCollection<Attachment>() 
					: new ObservableCollection<Attachment>(ProjectTask.Attachments);
				CommentListViewModel.Comments = ProjectTask.Comments == null 
					? new ObservableCollection<Comment>() 
					: new ObservableCollection<Comment>(ProjectTask.Comments);
				LinkListViewModel.Links = ProjectTask.Links == null 
					? new ObservableCollection<Link>() 
					: new ObservableCollection<Link>(ProjectTask.Links);
				SubTaskListViewModel.SubTasks = ProjectTask.SubTasks == null 
					? new ObservableCollection<SubTask>() 
					: new ObservableCollection<SubTask>(ProjectTask.SubTasks);
			}
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

		private bool isExpanded;

		public bool IsExpanded
		{
			get { return isExpanded; }
			set { SetProperty(ref isExpanded, value); }
		}

		private AddEditLinkViewModel addEditLinkViewModel;

		public AddEditLinkViewModel AddEditLinkViewModel
		{
			get { return addEditLinkViewModel; }
			set { SetProperty(ref addEditLinkViewModel, value); }
		}

		private AddEditAttachmentViewModel addEditAttachmentViewModel;

		public AddEditAttachmentViewModel AddEditAttachmentViewModel
		{
			get { return addEditAttachmentViewModel; }
			set { SetProperty(ref addEditAttachmentViewModel, value); }
		}

		private AddEditCommentViewModel addEditCommentViewModel;

		public AddEditCommentViewModel AddEditCommentViewModel
		{
			get { return addEditCommentViewModel; }
			set { SetProperty(ref addEditCommentViewModel, value); }
		}

		private AddEditTaskViewModel addEditTaskViewModel;

		public AddEditTaskViewModel AddEditTaskViewModel
		{
			get { return addEditTaskViewModel; }
			set { SetProperty(ref addEditTaskViewModel, value); }
		}

		private AddEditSubTaskViewModel addEditSubTaskViewModel;

		public AddEditSubTaskViewModel AddEditSubTaskViewModel
		{
			get { return addEditSubTaskViewModel; }
			set { SetProperty(ref addEditSubTaskViewModel, value); }
		}

		private AttachmentListViewModel attachmentListViewModel;

		public AttachmentListViewModel AttachmentListViewModel
		{
			get { return attachmentListViewModel; }
			set { SetProperty(ref attachmentListViewModel, value); }
		}

		private CommentListViewModel commentListViewModel;

		public CommentListViewModel CommentListViewModel
		{
			get { return commentListViewModel; }
			set { SetProperty(ref commentListViewModel, value); }
		}

		private LinkListViewModel linkListViewModel;

		public LinkListViewModel LinkListViewModel
		{
			get { return linkListViewModel; }
			set { SetProperty(ref linkListViewModel, value); }
		}

		private SubTaskListViewModel subTaskListViewModel;

		public SubTaskListViewModel SubTaskListViewModel
		{
			get { return subTaskListViewModel; }
			set { SetProperty(ref subTaskListViewModel, value); }
		}

		public RelayCommand EditTaskCommand { get; private set; }
		public RelayCommand ClosePopUpCommand { get; private set; }
		public RelayCommand AddAttachmentCommand { get; private set; }
		public RelayCommand AddCommentCommand { get; private set; }
		public RelayCommand AddLinkCommand { get; private set; }
		public RelayCommand AddSubTaskCommand { get; private set; }
		public RelayCommand DeleteTaskCommand { get; private set; }
		public RelayCommand ExpandAllCommand { get; private set; }
		public RelayCommand CloseTaskCommand { get; set; }

		public Action Close = delegate { };

		private async void DeleteTask()
		{
			MessageBoxResult result = MessageBox.Show("Do you really want to delete this Task?", "", MessageBoxButton.YesNo);
			if (result == MessageBoxResult.Yes)
			{
				await taskRepository.Delete(ProjectTask.Id);
			}
			Close();
		}

		private void CloseTask()
		{
			taskRepository.CloseProjectTask(ProjectTask.Id);
			Close();
		}

		private void AddSubTask()
		{
			AddEditSubTaskViewModel.ProjectTaskId = ProjectTask.Id;
			PopUpViewModel.CurrentViewModel = AddEditSubTaskViewModel;
			IsPopUpVisible = true;
		}

		private void AddLink()
		{
			AddEditLinkViewModel.Link = new Link { ProjectTaskId = ProjectTask.Id };
			PopUpViewModel.CurrentViewModel = AddEditLinkViewModel;
			IsPopUpVisible = true;
		}

		private void AddComment()
		{
			AddEditCommentViewModel.Comment = new Comment { ProjectTaskId = ProjectTask.Id };
			PopUpViewModel.CurrentViewModel = AddEditCommentViewModel;
			IsPopUpVisible = true;
		}

		private void AddAttachment()
		{
			AddEditAttachmentViewModel.TaskId = ProjectTask.Id;
			PopUpViewModel.CurrentViewModel = AddEditAttachmentViewModel;
			IsPopUpVisible = true;
		}

		private void ClosePopUpView()
		{
			PopUpViewModel.CurrentViewModel = null;
			IsPopUpVisible = false;
			LoadTaskFromDatbase(ProjectTask.Id);
		}

		private async void LoadTaskFromDatbase(int id)
		{
			ProjectTask = await taskRepository.GetProjectTaskByIdAsync(id);
		}

		private void OnEditTask()
		{
			PopUpViewModel.CurrentViewModel = AddEditTaskViewModel;
			AddEditTaskViewModel.EditMode = true;
			AddEditTaskViewModel.SetTask(ProjectTask);
			IsPopUpVisible = true;
		}

		private void TaskAdded(ProjectTask obj)
		{
			ProjectTask = obj;
			ClosePopUpView();
		}

		private void ExpandAll()
		{
			IsExpanded = !IsExpanded;
		}
	}
}