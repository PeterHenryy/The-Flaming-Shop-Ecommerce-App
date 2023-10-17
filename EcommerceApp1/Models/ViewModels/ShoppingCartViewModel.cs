using System.Collections.Generic;

namespace EcommerceApp1.Models.ViewModels
{
    public class ShoppingCartViewModel
    {
        public IEnumerable<CartItem> CartItems { get; set; }
        public List<DeliveryOption> DeliveryOptions { get; set; }
    }
}
