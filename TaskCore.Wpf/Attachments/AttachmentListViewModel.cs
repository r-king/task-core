using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using TaskCore.Data;
using TaskCore.Data.Interfaces;
using TaskCore.Domain;

namespace TaskCore.Wpf.Attachments
{
	public class AttachmentListViewModel : BindableBase
	{
		private IAttachmentRepository AttachmentRepository { get; set; }

		public AttachmentListViewModel(IAttachmentRepository attachmentRepository)
		{
			this.AttachmentRepository = attachmentRepository;
			DeleteAttachmentCommand = new RelayCommand<Attachment>(DeleteAttachment);

			OpenAttachmentCommand = new RelayCommand<Attachment>(OpenAttachment);
			DownloadAttachmentCommand = new RelayCommand<Attachment>(DownloadAttachment);

			Actions = new ObservableCollection<ContextAction<Attachment>>
			{
				new ContextAction<Attachment>
				{
					Name = "Open Attachment",
					Action = OpenAttachmentCommand
				},

				new ContextAction<Attachment>
				{
					Name = "Download Attachment",
					Action = DownloadAttachmentCommand
				}
			};
		}

		private async void DeleteAttachment(Attachment obj)
		{
			Attachments.Remove(obj);
			await AttachmentRepository.Delete(obj.Id);
		}

		private ObservableCollection<Attachment> attachments;

		public ObservableCollection<Attachment> Attachments
		{
			get { return attachments; }
			set { SetProperty(ref attachments, value); }
		}

		private ObservableCollection<ContextAction<Attachment>> actions;

		public ObservableCollection<ContextAction<Attachment>> Actions
		{
			get { return actions; }
			set { SetProperty(ref actions, value); }
		}

		private Attachment selectedAttachment;

		public Attachment SelectedAttachment
		{
			get { return selectedAttachment; }
			set { SetProperty(ref selectedAttachment, value); }
		}

		public RelayCommand<Attachment> DeleteAttachmentCommand { get; private set; }
		public RelayCommand<Attachment> OpenAttachmentCommand { get; private set; }
		public RelayCommand<Attachment> DownloadAttachmentCommand { get; private set; }

		private void DownloadAttachment(Attachment obj)
		{
			SaveFileDialog file = new SaveFileDialog();
			file.FileName = obj.Name;
			file.Filter = "All files (*.*)|*.*";
			file.ShowDialog();

			if (file.FileName != "")
			{
				File.WriteAllBytes(file.FileName, obj.AttachmentFile);
			}
		}

		private void OpenAttachment(Attachment obj)
		{
			string tempPath = Path.GetTempPath();
			string tempFilePath = $"{tempPath}/{obj.Name}";

			File.WriteAllBytes(tempFilePath, obj.AttachmentFile);
			Process.Start(tempFilePath);
		}
	}
}