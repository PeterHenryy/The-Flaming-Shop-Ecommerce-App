using System.Collections;
using System.Collections.Generic;

namespace EcommerceApp1.Models.IRepositories
{
    public interface IShoppingCartRepository
    {
        bool DeleteCartItem(int itemID);
        bool AddItemToCart(int itemID, int quantity, int userID);
        bool ClearCart();
        bool DeleteCart();
        bool UpdateCartItem(CartItem item);
        IEnumerable<CartItem> GetCartItems();
        ShoppingCart GetUserCart(int userID);
        CartItem GetCartItemByID(int itemID, int userID);
        double CalculateCartTotal();
    }
}
