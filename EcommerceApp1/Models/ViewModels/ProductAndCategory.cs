using System.Collections.Generic;

namespace EcommerceApp1.Models.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
    }
}
