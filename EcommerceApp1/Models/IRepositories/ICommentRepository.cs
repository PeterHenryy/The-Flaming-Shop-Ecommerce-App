namespace EcommerceApp1.Models.IRepositories
{
    public interface ICommentRepository
    {
        bool Create(Comment comment);
        bool Update(Comment comment);
        bool Delete(int commentID);
        Comment GetCommentByID(int commentID);
    }
}
