using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp1.Models
{
    public class CartItem
    {
        [Key]
        public int ID { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("ShoppingCarts")]
        public int CartID { get; set; }

        [ForeignKey("Products")]
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
