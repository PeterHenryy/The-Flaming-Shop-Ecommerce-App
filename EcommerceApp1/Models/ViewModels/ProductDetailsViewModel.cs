using System.Collections.Generic;

namespace EcommerceApp1.Models.ViewModels
{
    public class ProductDetailsViewModel
    {
        public Product Product { get; set; }
        public List<Review> Reviews { get; set; }
        public bool HasUserBoughtProduct { get; set; }
    }
}
