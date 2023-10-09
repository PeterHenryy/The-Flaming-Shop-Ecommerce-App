﻿using EcommerceApp1.Models;
using EcommerceApp1.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EcommerceApp1.Services
{
    public class ShoppingCartService
    {
        private readonly ShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartService(ShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }
        public bool AddItemToCart(int itemID, int quantity)
        {
            bool addedItem = _shoppingCartRepository.AddItemToCart(itemID, quantity);
            return addedItem;
        }

        public bool ClearCart()
        {
            bool clearedCart = _shoppingCartRepository.ClearCart();
            return clearedCart;
        }

        public bool DeleteCartItem(int itemID)
        {
            bool deletedItem = _shoppingCartRepository.DeleteCartItem(itemID);
            return deletedItem;
        }

        public CartItem GetCartItemByID(int itemID)
        {
            CartItem item = _shoppingCartRepository.GetCartItemByID(itemID);
            return item;
        }

        public IEnumerable<CartItem> GetCartItems()
        {
            var cartItems = _shoppingCartRepository.GetCartItems();
            return cartItems;
        }

        public ShoppingCart GetUserCart()
        {
            ShoppingCart userCart = _shoppingCartRepository.GetUserCart();
            return userCart;
        }

        public bool UpdateCartItemQuantity(int itemID, int quantity)
        {
            bool updatedItem = _shoppingCartRepository.UpdateCartItemQuantity(itemID, quantity);
            return updatedItem;
        }

        public double CalculateCartTotal()
        {
            double total = _shoppingCartRepository.CalculateCartTotal();
            return total;
        }
    }
}