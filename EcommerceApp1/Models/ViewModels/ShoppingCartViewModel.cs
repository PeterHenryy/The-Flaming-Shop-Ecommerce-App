using System.Collections.Generic;

namespace EcommerceApp1.Models.ViewModels
{
    public class ShoppingCartViewModel
    {
        public IEnumerable<CartItem>CartItems { get; set; }
        public double Total { get; set; }
    }
}
