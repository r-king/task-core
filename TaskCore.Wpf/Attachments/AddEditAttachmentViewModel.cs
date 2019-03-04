using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using TaskCore.Data;
using TaskCore.Data.Interfaces;
using TaskCore.Domain;

namespace TaskCore.Wpf.Attachments
{
	public class AddEditAttachmentViewModel : BindableBase
	{
		private IAttachmentRepository attachmentRepository { get; set; }

		public AddEditAttachmentViewModel(IAttachmentRepository attachmentRepository)
		{
			this.attachmentRepository = attachmentRepository;

			CancelCommand = new RelayCommand(Close);
			BrowseFilesCommand = new RelayCommand(BrowseFiles);
			AddAttachmentCommand = new RelayCommand(SaveAttachments);
			Attachments = new ObservableCollection<Attachment>();
		}

		private void Close()
		{
			Attachments = new ObservableCollection<Attachment>();
			Done();
		}

		private int taskId;

		public int TaskId
		{
			get { return taskId; }
			set { SetProperty(ref taskId, value); }
		}

		private ObservableCollection<Attachment> attachments;

		public ObservableCollection<Attachment> Attachments
		{
			get { return attachments; }
			set { SetProperty(ref attachments, value); }
		}

		public RelayCommand AddAttachmentCommand { get; private set; }
		public RelayCommand CancelCommand { get; private set; }
		public RelayCommand BrowseFilesCommand { get; private set; }

		public event Action Done = delegate { };

		public event Action<Attachment> AttachmentAdded = delegate { };

		private async void SaveAttachments()
		{
			foreach (var item in Attachments)
			{
				item.AttachmentFile = File.ReadAllBytes(item.Name);
				item.Name = Path.GetFileName(item.Name);
				item.Size = item.AttachmentFile.Length;
				await attachmentRepository.Add(item);
			}
			Close();
		}

		private void BrowseFiles()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Multiselect = true
			};

			if ((bool)openFileDialog.ShowDialog())
			{
				foreach (String file in openFileDialog.FileNames)
				{
					Attachments.Add(new Attachment
					{
						Name = file,
						ProjectTaskId = TaskId
					});
				}
			}
		}
	}
}