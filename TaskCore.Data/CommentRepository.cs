using TaskCore.Data.Interfaces;
using TaskCore.Domain;

namespace TaskCore.Data
{
	public class CommentRepository : BaseRepository<Comment>, ICommentRepository
	{
	}
}