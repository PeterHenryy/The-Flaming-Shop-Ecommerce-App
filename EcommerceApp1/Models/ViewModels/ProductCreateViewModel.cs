using EcommerceApp1.Models.Identity;
using System.Collections.Generic;

namespace EcommerceApp1.Models.ViewModels
{
    public class ProductCreateViewModel
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
        public AppUser User { get; set; }
    }
}
