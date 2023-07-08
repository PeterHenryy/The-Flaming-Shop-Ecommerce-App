﻿using EcommerceApp1.Models;
using EcommerceApp1.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp1.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
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
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            var createdProduct = _productService.Create(product);
            if (createdProduct)
            {
                return RedirectToAction("Index", "Product");
            }
            return View(product);
        }
    }
}
