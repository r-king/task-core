using TaskCore.Data;
using TaskCore.Data.Interfaces;
using Unity;
using Unity.Lifetime;

namespace TaskCore.Wpf
{
	public static class ContainerHelper
	{
		private static IUnityContainer _container;

		static ContainerHelper()
		{
			_container = new UnityContainer();
			_container.RegisterType<IProjectRepository, ProjectRepository>(new ContainerControlledLifetimeManager());
			_container.RegisterType<ITaskRepository, TaskRepository>(new ContainerControlledLifetimeManager());
			_container.RegisterType<IAttachmentRepository, AttachmentRepository>(new ContainerControlledLifetimeManager());
			_container.RegisterType<ICommentRepository, CommentRepository>(new ContainerControlledLifetimeManager());
			_container.RegisterType<ILinkRepository, LinkRepository>(new ContainerControlledLifetimeManager());
			_container.RegisterType<ISubTaskRepository, SubTaskRepository>(new ContainerControlledLifetimeManager());
		}

		public static IUnityContainer Container
		{
			get { return _container; }
		}
	}
}