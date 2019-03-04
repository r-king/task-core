using System;
using TaskCore.Data;
using TaskCore.Data.Interfaces;
using TaskCore.Domain;

namespace TaskCore.Wpf.Comments
{
	public class AddEditCommentViewModel : BindableBase
	{
		public ICommentRepository commentRepository;

		public AddEditCommentViewModel(ICommentRepository commentRepository)
		{
			this.commentRepository = commentRepository;			
			CancelCommand = new RelayCommand(Close);
			AddCommentCommand = new RelayCommand(AddComment);
		}

		private Comment comment;

		public Comment Comment
		{
			get { return comment; }
			set { SetProperty(ref comment, value); }
		}

		public RelayCommand CancelCommand { get; set; }
		public RelayCommand AddCommentCommand { get; set; }

		public event Action Done = delegate { };

		private void Close()
		{
			Done();
		}

		private async void AddComment()
		{
			await commentRepository.Add(Comment);
			Done();
		}
	}
}