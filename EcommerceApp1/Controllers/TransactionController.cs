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
            currentTransaction.CouponCode = couponCode;
            double cartTotal = _shoppingCartService.CalculateCartTotal();
            IEnumerable<CartItem> cartItems = _shoppingCartService.GetCartItems();
            _transactionService.CalculateTransactionTotal(cartTotal, currentTransaction, cartItems);
            transactionViewModel.UserCards = _transactionService.GetSpecificUserCards(currentUser.Id);
            transactionViewModel.CartItems = cartItems;
            transactionViewModel.Categories = _transactionService.GetCategories();
            transactionViewModel.ItemsBought = _shoppingCartService.GetItemsBoughtQuantity();
            transactionViewModel.TransactionTax = _transactionService.CalculateTransactionTax(cartTotal);
            return View(transactionViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransactionViewModel transactionViewModel)
        {
            AppUser currentUser = _userService.GetCurrentUser();
            Transaction currentTransaction = transactionViewModel.Transaction;
            currentTransaction.UserID = currentUser.Id;
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
            currentTransaction.ItemsBought = _shoppingCartService.GetItemsBoughtQuantity();
            bool createdTransaction = _transactionService.Create(currentTransaction);
            if (createdTransaction)
            {
                IEnumerable<CartItem> cartItems = _shoppingCartService.GetCartItems();
                for(int i = 0; i < cartItems.Count(); i++)
                {
                    CartItem cartItem = cartItems.ElementAt(i);
                    double itemCost = cartItem.Quantity * cartItem.Product.Price;
                    _transactionService.UpdateProductStock(cartItem.ProductID, cartItem.Quantity);
                    _transactionService.UpdateCompanyProperties(cartItem.Product.CompanyID, itemCost, cartItem.Quantity);
                    _transactionService.CreateTransactionItem(cartItem, currentTransaction.ID);
                }
                bool clearedCart = _shoppingCartService.ClearCart();
                await UpdateUserRewardPoints(currentTransaction.Total, currentUser, pointsPayment);
                return RedirectToAction("UserTransactions", "Transaction", new {userID = currentUser.Id});
            }
            return View(transactionViewModel);
        }

        [HttpGet]
        public IActionResult ValidateCoupon(string couponCode, Transaction transaction = null)
        {
            double cartTotal = _shoppingCartService.CalculateCartTotal();
            transaction.Total = cartTotal;
            IEnumerable<CartItem>cartItems = _shoppingCartService.GetCartItems();
            CouponValidator validatedCoupon = _transactionService.ValidateCoupon(transaction, cartItems, couponCode);

            return Json(validatedCoupon);
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
