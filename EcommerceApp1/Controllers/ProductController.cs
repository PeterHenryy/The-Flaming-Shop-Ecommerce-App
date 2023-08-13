﻿using EcommerceApp1.Models;
using EcommerceApp1.Models.Identity;
using EcommerceApp1.Models.ViewModels;
using EcommerceApp1.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EcommerceApp1.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private readonly UserService _userService;
        private readonly AppUser _user;

        public ProductController(ProductService productService, UserService userService)
        {
            _productService = productService;
            _userService = userService;
            _user = userService.GetCurrentUser();
        }

        public IActionResult Index()
        {
            var productIndexViewModel = new ProductIndexViewModel();
            productIndexViewModel.Products = _productService.GetAllProducts().ToList();
            var reviews = _productService.GetReviews();
            return View(productIndexViewModel);
        }

        public IActionResult ProductsDisplay()
        {
            var productIndexViewModel = new ProductIndexViewModel();
            productIndexViewModel.Products = _productService.GetAllProducts().ToList();
            var reviews = _productService.GetReviews();
            return View(productIndexViewModel);
        }

        public IActionResult Delete(int productID)
        {
            _productService.Delete(productID);
            return RedirectToAction("CompanyProducts", "Product");
        }

        [HttpGet]
        public IActionResult Update(int productID)
        {
            Product product = _productService.GetProductByID(productID);
            return View(product);
        }

        [HttpPost]
        public IActionResult Update(Product product)
        {
            var files = HttpContext.Request.Form.Files;
            if(files.Count > 0)
            {
                product.Image = Guid.NewGuid().ToString() + Path.GetExtension(files[0].FileName);
            }
            _productService.CheckProductStockChange(product.ID, product.Stock);
            bool updatedProduct = _productService.Update(product);
            _productService.HandleProductImages(product, files);
            if (updatedProduct)
            {
                return RedirectToAction("CompanyProducts", "Product");
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ProductCreateViewModel createViewModel = new ProductCreateViewModel();
            createViewModel.Categories = _productService.GetAllCategories();
            createViewModel.User = _user;
            return View(createViewModel);
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            var files = HttpContext.Request.Form.Files;
            product.Image = Guid.NewGuid().ToString() + Path.GetExtension(files[0].FileName);
            product.UserID = _user.Id;
            bool createdProduct = _productService.Create(product);
            _productService.HandleProductImages(product, files);
            if (createdProduct)
            {
                bool updatedStock = _productService.UpdateCompanyProductStock(product.CompanyID, product.Stock, "increase");
                return RedirectToAction("CompanyProducts", "Product");
            }

            return View(product);
        }

        public IActionResult Details(int productID)
        {
            Product product = _productService.GetProductByID(productID);
            var detailsViewModel = new ProductDetailsViewModel();
            detailsViewModel.Product = product;
            detailsViewModel.Reviews = _productService.GetReviewsOfSpecificProduct(productID).ToList();
            detailsViewModel.HasUserBoughtProduct = _productService.HasUserBoughtProduct(productID, _user.Id);
            detailsViewModel.Comments = _productService.GetAllComments().ToList();
            detailsViewModel.Likes = _productService.GetLikes().ToList();
            detailsViewModel.Dislikes = _productService.GetDislikes().ToList();
            detailsViewModel.CurrentUser = _user;
            detailsViewModel.ProductID = productID;
            detailsViewModel.HasUserReviewedProduct = _productService.HasUserReviewedProduct(productID, _user.Id);
            detailsViewModel.ProductImages = _productService.GetProductImages(productID);
            return View(detailsViewModel);
        }

        public IActionResult CompanyProducts()
        {
            var companyProducts = _productService.GetCompanyProducts(_user.CompanyID);
            return View(companyProducts);
        }

        public IActionResult ManageProductArchiving(int productID, int option)
        {
            _productService.ManageProductArchiving(productID, option);
            return RedirectToAction("CompanyProducts", "Product");
        }
      
    }
}
