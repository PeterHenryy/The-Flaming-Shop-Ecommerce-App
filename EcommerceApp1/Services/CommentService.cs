using EcommerceApp1.Models;
using EcommerceApp1.Models.Repositories;

namespace EcommerceApp1.Services
{
    public class CommentService
    {
        private readonly CommentRepository _commentRepos;

        public CommentService(CommentRepository commentRepos)
        {
            _commentRepos = commentRepos;
        }

        public bool Create(Comment comment)
        {
            bool createdComment = _commentRepos.Create(comment);
            return createdComment;
        }

        public bool Update(Comment comment)
        {
            bool updatedComment = _commentRepos.Update(comment);
            return updatedComment;
        }

        public bool Delete(int commentID)
        {
            bool deletedComment = _commentRepos.Delete(commentID);
            return deletedComment;
        }

        public Comment GetCommentByID(int commentID)
        {
            Comment comment = _commentRepos.GetCommentByID(commentID);
            return comment;
        }

    }
}
