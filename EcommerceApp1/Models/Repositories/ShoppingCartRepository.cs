using EcommerceApp1.Data;
using EcommerceApp1.Models.Identity;
using EcommerceApp1.Models.IRepositories;
using EcommerceApp1.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceApp1.Models.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly AppUser _currentUser;

        public ShoppingCartRepository(ApplicationDbContext context, UserService userService)
        {
            _context = context;
            _currentUser = userService.GetCurrentUser();
        }
        public bool AddItemToCart(int itemID, int quantity)
        {
            CartItem cartItem = GetCartItemByID(itemID);
            if(cartItem != null)
            {
                cartItem.Quantity += quantity;
                bool updatedItem = UpdateCartItemQuantity(itemID, cartItem.Quantity);
            }
            else
            {
                ShoppingCart userCart = GetUserCart();
                cartItem = new CartItem
                {
                    Quantity = quantity,
                    CartID = userCart.ID,
                    ProductID = itemID
                };
            }
            try
            {
                _context.CartItems.Add(cartItem);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public bool ClearCart()
        {
            try
            {
                ShoppingCart userCart = GetUserCart();
                var cartItems = GetCartItems();
                _context.CartItems.RemoveRange(cartItems);
                _context.ShoppingCarts.Remove(userCart);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public bool DeleteCartItem(int itemID)
        {
            try
            {
                CartItem item = GetCartItemByID(itemID);
                _context.CartItems.Remove(item);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public CartItem GetCartItemByID(int itemID)
        {
            ShoppingCart userCart = GetUserCart();
            CartItem item = _context.CartItems.Where(x => x.CartID == userCart.ID).SingleOrDefault(x => x.ProductID == itemID);
            return item;
        }

        public IEnumerable<CartItem> GetCartItems()
        {
            var cartItems = _context.CartItems.Where(x => x.CartID == GetUserCart().ID).Include(x => x.Product);
            return cartItems;
        }

        public ShoppingCart GetUserCart()
        {
            ShoppingCart userCart = _context.ShoppingCarts.SingleOrDefault(x => x.UserID == _currentUser.Id);
            if(userCart == null)
            {
                userCart = new ShoppingCart
                {
                    UserID = _currentUser.Id
                };
                _context.ShoppingCarts.Add(userCart);
                _context.SaveChanges();
                return _context.ShoppingCarts.SingleOrDefault(x => x.UserID == _currentUser.Id);
            }
            return userCart;
        }

        public bool UpdateCartItemQuantity(int itemID, int quantity)
        {
            try
            {
                CartItem item = GetCartItemByID(itemID);
                item.Quantity = quantity;
                _context.CartItems.Update(item);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
