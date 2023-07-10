using System.ComponentModel.DataAnnotations;

namespace EcommerceApp1.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

    }
}
