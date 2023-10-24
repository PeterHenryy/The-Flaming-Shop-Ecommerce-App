using EcommerceApp1.Models;
using EcommerceApp1.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
            CartItem item = _shoppingCartRepository.GetCartItemByID(itemID);
            item.Quantity = quantity;
            bool updatedQuantity = _shoppingCartRepository.UpdateCartItem(item);
            return updatedQuantity;
        }

        public double CalculateCartTotal()
        {
            double total = _shoppingCartRepository.CalculateCartTotal();
            return total;
        }
        public bool UpdateCartItemShippingOption(int itemID, double newShippingCost, string shippingOption)
        {
            CartItem item = GetCartItemByID(itemID);
            item.ShippingCost = newShippingCost;
            item.ShippingOption = shippingOption;
            bool updatedShippingCost = _shoppingCartRepository.UpdateCartItem(item);
            return updatedShippingCost;
        }

        public List<DeliveryOption> GetDeliveryOptions()
        {
            List<DeliveryOption> options = _shoppingCartRepository.GetDeliveryOptions();
            return options;
        }

        public int GetItemsBoughtQuantity()
        {
            IEnumerable<CartItem> cartItems = _shoppingCartRepository.GetCartItems();
            int itemsBought = cartItems.Sum(x => x.Quantity);
            return itemsBought;
        }
    }
        
}
