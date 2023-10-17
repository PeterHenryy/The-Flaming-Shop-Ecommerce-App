using System.ComponentModel.DataAnnotations;

namespace EcommerceApp1.Models
{
    public class DeliveryOption
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }

    }
}
