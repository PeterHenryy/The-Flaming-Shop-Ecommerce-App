using EcommerceApp1.Data;
using EcommerceApp1.Models.IRepositories;
using System.Linq;

namespace EcommerceApp1.Models.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Create(Comment comment)
        {
            try
            {
                _context.Comments.Add(comment);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }

        public bool Delete(int commentID)
        {
            try
            {
                Comment comment = GetCommentByID(commentID);
                _context.Comments.Remove(comment);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }

        public Comment GetCommentByID(int commentID)
        {
            Comment comment = _context.Comments.SingleOrDefault(x => x.ID == commentID);
            return comment;
        }

        public bool Update(Comment comment)
        {
            try
            {
                _context.Comments.Update(comment);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }
    }
}
