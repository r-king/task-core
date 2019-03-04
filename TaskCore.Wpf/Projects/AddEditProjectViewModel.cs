using System;
using TaskCore.Data.Interfaces;
using TaskCore.Domain;

namespace TaskCore.Wpf.Projects
{
	public class AddEditProjectViewModel : BindableBase
	{
		private Project _editingProject;
		private bool _editMode;
		private EditableProject _project;
		private IProjectRepository _repository;

		public AddEditProjectViewModel(IProjectRepository repository)
		{
			_repository = repository;
			SaveProjectCommand = new RelayCommand(SaveProject);
			CancelProjectCommand = new RelayCommand(CancelProject);
		}

		public event Action Done = delegate { };

		public RelayCommand CancelProjectCommand { get; private set; }

		public Project EditingProject
		{
			get { return _editingProject; }
			set { SetProperty(ref _editingProject, value); }
		}

		public bool EditMode
		{
			get { return _editMode; }
			set { SetProperty(ref _editMode, value); }
		}

		public EditableProject Project
		{
			get { return _project; }
			set { SetProperty(ref _project, value); }
		}

		public RelayCommand SaveProjectCommand { get; private set; }

		public void SetProject(Project project)
		{
			_editingProject = project;
			Project = new EditableProject();
			CopyProject(project, Project);
		}

		private void CancelProject()
		{
			Done();
		}

		private void CopyProject(Project source, EditableProject target)
		{
			target.Id = source.Id;
			if (EditMode)
			{
				target.Name = source.Name;
				target.Description = source.Description;
			}
		}

		private async void SaveProject()
		{
			UpdateProject(Project, _editingProject);
			if (EditMode)
			{
				await _repository.UpdateAsync(_editingProject);
			}
			else
			{
				await _repository.Add(_editingProject);
			}
			Done();
		}

		private void UpdateProject(EditableProject source, Project target)
		{
			target.Name = source.Name;
			target.Description = source.Description;
		}
	}
}