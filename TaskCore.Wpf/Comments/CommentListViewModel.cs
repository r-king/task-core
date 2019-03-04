using System.Collections.ObjectModel;
using TaskCore.Data;
using TaskCore.Data.Interfaces;
using TaskCore.Domain;

namespace TaskCore.Wpf.Comments
{
	public class CommentListViewModel : BindableBase
	{
		public ICommentRepository commentRepository { get; set; }

		public CommentListViewModel(ICommentRepository commentRepository)
		{
			this.commentRepository = commentRepository;
			DeleteCommentCommand = new RelayCommand<Comment>(DeleteComment);
		}

		private void DeleteComment(Comment obj)
		{
			Comments.Remove(obj);
			commentRepository.Delete(obj.Id);
		}

		private ObservableCollection<Comment> comments;

		public ObservableCollection<Comment> Comments
		{
			get { return comments; }
			set { SetProperty(ref comments, value); }
		}

		public RelayCommand<Comment> DeleteCommentCommand { get; private set; }
	}
}