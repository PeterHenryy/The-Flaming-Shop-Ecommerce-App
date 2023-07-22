using EcommerceApp1.Models;
using EcommerceApp1.Models.ViewModels;
using EcommerceApp1.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EcommerceApp1.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private readonly UserService _userService;

        public ProductController(ProductService productService, UserService userService)
        {
            _productService = productService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            return View(products);
        }

        public IActionResult Delete(int productID)
        {
            _productService.Delete(productID);
            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public IActionResult Update(int productID)
        {
            var product = _productService.GetProductByID(productID);
            return View(product);
        }

        [HttpPost]
        public IActionResult Update(Product product)
        {
            var updatedProduct = _productService.Update(product);
            if (updatedProduct)
            {
                return RedirectToAction("Index", "Product");
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ProductCreateViewModel createViewModel = new ProductCreateViewModel();
            createViewModel.Categories = _productService.GetAllCategories();
            createViewModel.Companies = _productService.GetAllCompanies();
            return View(createViewModel);
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            var currentUser = _userService.GetCurrentUser();
            product.UserID = currentUser.Id;
            var createdProduct = _productService.Create(product);
            if (createdProduct)
            {
                return RedirectToAction("Index", "Product");
            }
            return View(product);
        }

        public IActionResult Details(int productID)
        {
            var currentUser = _userService.GetCurrentUser();
            var product = _productService.GetProductByID(productID);
            var detailsViewModel = new ProductDetailsViewModel();
            detailsViewModel.Product = product;
            detailsViewModel.Reviews = _productService.GetReviewsOfSpecificProduct(productID).ToList();
            detailsViewModel.HasUserBoughtProduct = _productService.HasUserBoughtProduct(productID, currentUser.Id);
            detailsViewModel.Comments = _productService.GetAllComments().ToList();
            detailsViewModel.Likes = _productService.GetLikes().ToList();
            detailsViewModel.Dislikes = _productService.GetDislikes().ToList();
            detailsViewModel.CurrentUser = currentUser;
            detailsViewModel.ProductID = productID;
            detailsViewModel.HasUserReviewedProduct = _productService.HasUserReviewedProduct(productID, currentUser.Id);
            return View(detailsViewModel);
        }

      
    }
}
