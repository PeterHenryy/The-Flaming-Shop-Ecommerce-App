using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp1.Models
{
    public class Image
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        [ForeignKey("Products")]
        public int ProductID { get; set; }
    }
}
