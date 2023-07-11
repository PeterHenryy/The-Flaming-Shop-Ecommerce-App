﻿using EcommerceApp1.Models;
using EcommerceApp1.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EcommerceApp1.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepos;

        public ProductService(ProductRepository productRepos)
        {
            _productRepos = productRepos;
        }

        public bool Create(Product product)
        {
            bool createdProduct = _productRepos.Create(product);
            return createdProduct;
        }

        public bool Delete(int productID)
        {
            var deletedProduct = _productRepos.Delete(productID);
            return deletedProduct;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = _productRepos.GetAllProducts();
            return products;
        }

        public Product GetProductByID(int productID)
        {
            var product = _productRepos.GetProductByID(productID);
            return product;
        }

        public bool Update(Product product)
        {
            var updatedProduct = _productRepos.Update(product);
            return updatedProduct;
        }

        public List<Category> GetAllCategories()
        {
            var categories = _productRepos.GetAllCategories();
            return categories;
        }

    }
}
