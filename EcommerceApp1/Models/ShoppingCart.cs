using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp1.Models
{
    public class ShoppingCart
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("AspNetUsers")]
        public int UserID { get; set; }
    }
}
