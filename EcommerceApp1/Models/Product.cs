using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp1.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public string ReleaseDate { get; set; }

        [ForeignKey("Companies")]
        public int CompanyID { get; set; }
        public virtual Company Company { get; set; }

        [ForeignKey("AspNetUsers")]
        public int UserID { get; set; }
        

    }
}
