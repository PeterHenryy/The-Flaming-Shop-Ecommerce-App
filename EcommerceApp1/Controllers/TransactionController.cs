using EcommerceApp1.Helpers;
using EcommerceApp1.Helpers.Enums;
using EcommerceApp1.Helpers.Payments;
using EcommerceApp1.Models;
using EcommerceApp1.Models.Identity;
using EcommerceApp1.Models.ViewModels;
using EcommerceApp1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApp1.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly TransactionService _transactionService;
        private readonly UserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ShoppingCartService _shoppingCartService;

        public TransactionController(TransactionService transactionService, UserService userService, UserManager<AppUser> userManager, ShoppingCartService shoppingCartService)
        {
            _transactionService = transactionService;
            _userService = userService;
            _userManager = userManager;
            _shoppingCartService = shoppingCartService;
        }

        public IActionResult UserTransactions(int userID)
        {
            var userTransactionsViewModel = new UserTransactionsViewModel();
            userTransactionsViewModel.UserTransactions = _transactionService.GetTransactionsByUserID(userID);
            userTransactionsViewModel.UserRefunds = _transactionService.GetAllUserRefunds(userID);
            return View(userTransactionsViewModel);
        }

        [HttpGet]
        public IActionResult Create(string couponCode = null)
        {
            AppUser currentUser = _userService.GetCurrentUser();
            var transactionViewModel= new TransactionViewModel();
            transactionViewModel.Transaction = new Transaction();
            Transaction currentTransaction = transactionViewModel.Transaction;
            //currentTransaction.ProductID = productID;
            //currentTransaction.CurrentProduct = _transactionService.GetProductByID(productID);
            transactionViewModel.UserCards = _transactionService.GetSpecificUserCards(currentUser.Id);
            return View(transactionViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransactionViewModel transactionViewModel)
        {
            AppUser currentUser = _userService.GetCurrentUser();
            Transaction currentTransaction = transactionViewModel.Transaction;
            currentTransaction.UserID = currentUser.Id;
            if(currentTransaction.CouponCode != null)
            {
                ValidateCoupon(currentTransaction.CouponCode);
            }
            //else
            //{
            //    currentTransaction.Total = _transactionService.CalculateTransactionTotal(currentTransaction);
            //}
            transactionViewModel.UserCards = _transactionService.GetSpecificUserCards(currentUser.Id);
            bool pointsPayment = currentTransaction.PaymentType == PaymentTypes.RewardPoints.ToString();
            if (pointsPayment)
            {
                var pointsPaymentValidator = new PointsPayment();
                pointsPayment = pointsPaymentValidator.ValidatePointsForTransaction(currentUser, currentTransaction.Total);
                if (!pointsPayment)
                {
                    return View(transactionViewModel);
                }
            }
            else if (!currentUser.HasCreditCard)
            {
                return RedirectToAction("Create", "CreditCard");
            }

            bool createdTransaction = _transactionService.Create(currentTransaction);
            if (createdTransaction)
            {
                //_transactionService.UpdateProductStock(currentTransaction.ProductID, currentTransaction.QuantityBought);
                //_transactionService.UpdateCompanyProperties(currentTransaction.CurrentProduct.CompanyID, currentTransaction.Total, currentTransaction.QuantityBought);
                await UpdateUserRewardPoints(currentTransaction.Total, currentUser, pointsPayment);
                return RedirectToAction("UserTransactions", "Transaction", new {userID = currentUser.Id});
            }
            return View(transactionViewModel);
        }

        [HttpGet]
        public IActionResult ValidateCoupon(string couponCode)
        {
            IEnumerable<CartItem>cartItems =  _shoppingCartService.GetCartItems();
            double cartTotal =   _shoppingCartService.CalculateCartTotal();
            foreach(var item in cartItems)
            {
                Coupon coupon =  _transactionService.GetCoupon(couponCode, item.Product.CompanyID);
                CouponValidator couponValidator = new CouponValidator();
                bool isCouponValid = couponValidator.Validate(coupon, item.Product);
                if (isCouponValid)
                {
                    double transactionTotal = _transactionService.CalculateTransactionTotal(cartTotal, coupon.DiscountPercentage);
                    return Json(new CouponValidator {
                        Total = transactionTotal,
                        CouponValid = true,
                        CouponPercentage = coupon.DiscountPercentage
                    });
                }
            }
            return Json(new CouponValidator
            {
                Total = cartTotal,
                CouponValid = false
            });
        }

        public async Task UpdateUserRewardPoints(double transactionTotal, AppUser currentUser, bool paidWithPoints = false)
        {
            if (paidWithPoints)
            {
                currentUser.UserRewardPoints -= transactionTotal * 5;                
            }
            else
            {
                var rewardPoints = _userService.CalculateUserRewardPoints(transactionTotal);
                currentUser.UserRewardPoints += rewardPoints;
            }
            await _userManager.UpdateAsync(currentUser);
        }


    }
}
