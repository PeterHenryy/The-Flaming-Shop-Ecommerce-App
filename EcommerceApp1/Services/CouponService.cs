using EcommerceApp1.Models;
using EcommerceApp1.Models.Repositories;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceApp1.Services
{
    public class CouponService
    {
        private readonly CouponRepository _couponRepos;

        public CouponService(CouponRepository couponRepos)
        {
            _couponRepos = couponRepos;
        }

        public bool Create(Coupon coupon)
        {
            bool createdCoupon = _couponRepos.Create(coupon);
            return createdCoupon;
        }

        public bool Delete(int couponID)
        {
            bool deletedCoupon = _couponRepos.Delete(couponID);
            return deletedCoupon;
        }

        public IEnumerable<Coupon> GetCompanyCoupons(int companyID)
        {
            var companyCoupons = _couponRepos.GetCompanyCoupons(companyID).ToList();
            return companyCoupons;
        }

        public IEnumerable<Category> GetCompanyCategories(int companyID)
        {
            var companyProducts = _couponRepos.GetProducts().Where(x => x.CompanyID == companyID);
            var categories = _couponRepos.GetCategories();
            List<Category> companyCategories = new List<Category>();
            foreach(var category in categories)
            {
                if(companyProducts.Any(x => x.CategoryID == category.ID))
                {
                    companyCategories.Add(category);
                }
            }
            return companyCategories;
        }

        public IEnumerable<Product> GetCompanyProducts(int companyID)
        {
            var companyProducts = _couponRepos.GetProducts().Where(x => x.CompanyID == companyID);
            return companyProducts;
        }
    }
}
