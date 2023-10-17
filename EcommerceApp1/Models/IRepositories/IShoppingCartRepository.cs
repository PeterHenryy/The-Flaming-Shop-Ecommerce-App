using System.Collections;
using System.Collections.Generic;

namespace EcommerceApp1.Models.IRepositories
{
    public interface IShoppingCartRepository
    {
        bool DeleteCartItem(int itemID);
        bool AddItemToCart(int itemID, int quantity);
        bool ClearCart();
        bool DeleteCart();
        bool UpdateCartItem(CartItem item);
        IEnumerable<CartItem> GetCartItems();
        ShoppingCart GetUserCart();
        CartItem GetCartItemByID(int itemID);
        double CalculateCartTotal();
    }
}
