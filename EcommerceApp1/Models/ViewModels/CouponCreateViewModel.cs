using System.Collections.Generic;

namespace EcommerceApp1.Models.ViewModels
{
    public class CouponCreateViewModel
    {
        public Coupon Coupon { get; set; }
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
    }
}
