using EcommerceApp1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp1.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly ShoppingCartService _shoppingCartService;

        public ShoppingCartController(ShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        public IActionResult DisplayCartItems()
        {
            var items = _shoppingCartService.GetCartItems();
            return View(items);
        }

        public IActionResult AddItemToCart(int itemID, int quantity)
        {
            bool addedItem = _shoppingCartService.AddItemToCart(itemID, quantity);
            return RedirectToAction("DisplayCartItems");
        }

        public IActionResult ClearCart()
        {
            bool clearedCart = _shoppingCartService.ClearCart();
            return RedirectToAction("DisplayCartItems");
        }
    }
}
