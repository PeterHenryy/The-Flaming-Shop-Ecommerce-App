using EcommerceApp1.Helpers.Enums;
using EcommerceApp1.Models;
using EcommerceApp1.Models.Identity;
using EcommerceApp1.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcommerceApp1.Controllers
{
    public class RefundController : Controller
    {
        private readonly RefundService _refundService;
        private readonly UserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppUser _currentUser;

        public RefundController(RefundService refundService, UserService userService, UserManager<AppUser> userManager)
        {
            _refundService = refundService;
            _userService = userService;
            _userManager = userManager;
            _currentUser = userService.GetCurrentUser();
        }

        public IActionResult UserRefunds()
        {
            var userRefunds = _refundService.GetUserRefunds(_currentUser.Id);
            return View(userRefunds);
        }

        public IActionResult CompanyRefunds()
        {
            var companyRefunds = _refundService.GetCompanyRefunds(_currentUser.CompanyID);
            return View(companyRefunds);
        }

        [HttpGet]
        public IActionResult Create(int transactionID, int productID)
        {
            Refund refund = new Refund();
            refund.TransactionID = transactionID;
            refund.ProductID = productID;
            refund.UserID = _currentUser.Id;
            return View(refund);
        }

        [HttpPost]
        public IActionResult Create(Refund refund)
        {
            bool createdRefund = _refundService.Create(refund);
            if (createdRefund)
            {
                return RedirectToAction("UserTransactions", "Transaction", new {userID = _currentUser.Id});
            }
            return View(refund);
        }

        public async Task<IActionResult> UpdateRefundStatus(int refundID, bool accepted, double transactionTotal, string paymentType)
        {
            Refund refund = _refundService.GetRefundByID(refundID);
            bool refundAccepted = _refundService.HasAdminAcceptedRefund(accepted, refund);
            if (refundAccepted)
            {
                await RefundUserRewardPoints(transactionTotal, paymentType, refund.UserID);
            }
            _refundService.Update(refund);
            return RedirectToAction("CompanyRefunds", "Refund");
        }

        public async Task RefundUserRewardPoints(double transactionTotal, string paymentType, int? userID)
        {
            AppUser user = _userManager.FindByIdAsync(userID.ToString()).Result;
            if(paymentType == PaymentTypes.CreditCard.ToString())
            {
                user.UserRewardPoints -= transactionTotal / 2;
            }
            user.UserRewardPoints += transactionTotal * 5;
            await _userManager.UpdateAsync(user);
        }
    }
}
