using EcommerceApp1.Models;
using EcommerceApp1.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceApp1.Components
{
    public class ShoppingCartItems : ViewComponent
    {
        private readonly ShoppingCartService _shoppingCartService;

        public ShoppingCartItems(ShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        public IViewComponentResult Invoke()
        {
            var cartItems = _shoppingCartService.GetCartItems();
            return (cartItems == null) ? View(0) : View(cartItems.Sum(x => x.Quantity));
        }
    }
}
