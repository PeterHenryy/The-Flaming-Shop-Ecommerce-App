using EcommerceApp1.Models;
using EcommerceApp1.Models.Repositories;

namespace EcommerceApp1.Services
{
    public class RatingService
    {
        private readonly RatingRepository _ratingRepos;

        public RatingService(RatingRepository ratingRepos)
        {
            _ratingRepos = ratingRepos;
        }

        public bool CreateLike(Like like)
        {
            bool createdLike = _ratingRepos.CreateLike(like);
            return createdLike;
        }

        public bool CreateDislike(Dislike dislike)
        {
            bool createdDislike = _ratingRepos.CreateDislike(dislike);
            return createdDislike;
        }

        public bool DeleteLike(int likeID)
        {
            bool deletedLike = _ratingRepos.DeleteLike(likeID);
            return deletedLike;
        }

        public bool DeleteDislike(int dislikeID)
        {
            bool deletedDislike = _ratingRepos.DeleteDislike(dislikeID);
            return deletedDislike;
        }
    }
}
