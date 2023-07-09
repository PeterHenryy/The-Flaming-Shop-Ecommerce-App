using EcommerceApp1.Models;
using EcommerceApp1.Models.Identity;
using EcommerceApp1.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcommerceApp1.Controllers
{
    public class TransactionController : Controller
    {
        private readonly TransactionService _transactionService;
        private readonly UserService _userService;
        private readonly UserManager<AppUser> _userManager;

        public TransactionController(TransactionService transactionService, UserService userService, UserManager<AppUser> userManager)
        {
            _transactionService = transactionService;
            _userService = userService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var transactions = _transactionService.GetAllTransactions();
            return View(transactions);
        }

        [HttpGet]
        public IActionResult Create(int productID)
        {
            var transaction = new Transaction();
            transaction.ProductID = productID;
            transaction.CurrentProduct = _transactionService.GetProductByID(productID);
            return View(transaction);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Transaction transaction)
        {
            var currentUser = _userService.GetCurrentUser();
            transaction.UserID = currentUser.Id;
            transaction.Total = _transactionService.CalculateTransactionTotal(transaction);
            bool createdTransaction = _transactionService.Create(transaction);
            if (createdTransaction)
            {
                await UpdateUserRewardPoints(transaction.Total);
                return RedirectToAction("Index", "Transaction");
            }
            return View(transaction);
        }

        public async Task UpdateUserRewardPoints(double transactionTotal)
        {
            var rewardPoints = UserService.CalculateUserRewardPoints(transactionTotal);
            var currentUser = _userService.GetCurrentUser();
            currentUser.UserRewardPoints += rewardPoints;
            await _userManager.UpdateAsync(currentUser);
        }
    }
}
