using EcommerceApp1.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace EcommerceApp1.Helpers.Payments
{
    public class PointsPayment
    {
        public bool ValidatePointsForTransaction(AppUser user, double transactionTotal)
        {
            return user.UserRewardPoints >= transactionTotal * 5;
            
        }
    }
}
