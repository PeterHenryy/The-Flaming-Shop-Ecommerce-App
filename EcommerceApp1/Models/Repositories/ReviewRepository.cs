using EcommerceApp1.Data;
using EcommerceApp1.Models.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceApp1.Models.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Create(Review review)
        {
            try
            {
                _context.Reviews.Add(review);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }

        public bool Delete(int reviewID)
        {
            try
            {
                Review review = GetReviewByID(reviewID);
                _context.Reviews.Remove(review);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }

        public Review GetReviewByID(int reviewID)
        {
            Review review = _context.Reviews.SingleOrDefault(x => x.ID == reviewID);
            return review;
        }

        public IEnumerable<Review> GetSpecificUserReviews(int userID)
        {
            var userReviews = _context.Reviews.Include(x => x.Product).Where(x => x.UserID == userID);
            return userReviews;
        }

        public bool Update(Review review)
        {
            try
            {
                _context.Reviews.Update(review);
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
