using System.Collections.Generic;

namespace EcommerceApp1.Models.ViewModels
{
    public class CompanyStatsViewModel
    {
        public Company Company { get; set; }
        public double YearRevenue { get; set; }
        public int YearCustomers { get; set; }
        public int YearOrders { get; set; }
        public List<Review> CompanyProductsReviews { get; set; }
        public IEnumerable<TransactionItem> CompanyPurchasedItems { get; set; }
    }
}
