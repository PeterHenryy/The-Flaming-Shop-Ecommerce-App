using EcommerceApp1.Models;
using EcommerceApp1.Models.Identity;
using EcommerceApp1.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp1.Controllers
{
    public class CommentController : Controller
    {
        private readonly CommentService _commentService;
        private readonly UserService _userService;

        public CommentController(CommentService commentService, UserService userService)
        {
            _commentService = commentService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Comment comment)
        {
            AppUser user = _userService.GetCurrentUser();
            comment.UserID = user.Id;
            bool createdComment = _commentService.Create(comment);
            if (createdComment)
            {
                return RedirectToAction("Details", "Product", new {productID = comment.ProductID});
            }
            return View(createdComment);
        }

        [HttpGet]
        public IActionResult Update(int commentID)
        {
            Comment comment = _commentService.GetCommentByID(commentID);
            return View(comment);
        }

        [HttpPost]
        public IActionResult Update(Comment comment)
        {
            bool updatedComment = _commentService.Update(comment);
            if (updatedComment)
            {
                return RedirectToAction("Details", "Product", new { productID = comment.ProductID });
            }
            return View(updatedComment);
        }

        public IActionResult Delete(int commentID)
        {
            Comment comment = _commentService.GetCommentByID(commentID);
            _commentService.Delete(commentID);
            return RedirectToAction("Details", "Product", new { productID = comment.ProductID });
        }
    }
}
