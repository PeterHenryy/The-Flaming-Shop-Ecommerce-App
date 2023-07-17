using EcommerceApp1.Models;
using EcommerceApp1.Models.Repositories;
using System.Collections;
using System.Collections.Generic;

namespace EcommerceApp1.Services
{
    public class ReviewService
    {
        private readonly ReviewRepository _reviewRepos;

        public ReviewService(ReviewRepository reviewRepos)
        {
            _reviewRepos = reviewRepos;
        }

        public bool Create(Review review)
        {
            bool createdReview = _reviewRepos.Create(review);
            return createdReview;
        }

        public bool Delete(int reviewID)
        {
            bool deletedReview = _reviewRepos.Delete(reviewID);
            return deletedReview;
        }

        public bool Update(Review review)
        {
            bool updatedReview = _reviewRepos.Update(review);
            return updatedReview;
        }

        public IEnumerable<Review> GetUserReviews(int userID)
        {
            var userReviews = _reviewRepos.GetSpecificUserReviews(userID);
            return userReviews;
        }

        public Review GetReviewByID(int reviewID)
        {
            Review review = _reviewRepos.GetReviewByID(reviewID);
            return review;
        }
    }
}
