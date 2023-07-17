using EcommerceApp1.Models;
using EcommerceApp1.Models.Identity;
using EcommerceApp1.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp1.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ReviewService _reviewService;
        private readonly UserService _userService;

        public ReviewController(ReviewService reviewService, UserService userService)
        {
            _reviewService = reviewService;
            _userService = userService;
        }

        public IActionResult UserReviews()
        {
            AppUser user = _userService.GetCurrentUser();
            var userReviews = _reviewService.GetUserReviews(user.Id);
            return View(userReviews);
        }

        [HttpGet]
        public IActionResult Create(int productID)
        {
            var review = new Review();
            review.ProductID = productID;
            return View(review);
        }

        [HttpPost]
        public IActionResult Create(Review review)
        {
            AppUser user = _userService.GetCurrentUser();
            review.UserID = user.Id;
            bool createdReview = _reviewService.Create(review);
            if (createdReview)
            {
                return RedirectToAction("UserReviews", "Review");
            }
            return View(review);
        }

        [HttpGet]
        public IActionResult Update(int reviewID)
        {
            Review review = _reviewService.GetReviewByID(reviewID);
            return View(review);
        }

        [HttpPost]
        public IActionResult Update(Review review)
        {
            bool updatedReview = _reviewService.Update(review);
            if (updatedReview)
            {
                return RedirectToAction("UserReviews", "Review");
            }
            return View(review);
        }

        public IActionResult Delete(int reviewID)
        {
            Review review = _reviewService.GetReviewByID(reviewID);
            var deletedReview = _reviewService.Delete(reviewID);
            return RedirectToAction("UserReviews", "Review");
        }
    }
}
