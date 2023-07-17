using System.Collections.Generic;

namespace EcommerceApp1.Models.ViewModels
{
    public class ProductCreateViewModel
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
        public List<Company> Companies { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
