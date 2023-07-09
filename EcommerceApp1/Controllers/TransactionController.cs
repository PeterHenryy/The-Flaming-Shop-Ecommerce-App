using EcommerceApp1.Models;
using EcommerceApp1.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp1.Controllers
{
    public class TransactionController : Controller
    {
        private readonly TransactionService _transactionService;
        private readonly UserService _userService;

        public TransactionController(TransactionService transactionService, UserService userService)
        {
            _transactionService = transactionService;
            _userService = userService;
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
        public IActionResult Create(Transaction transaction)
        {
            var currentUser = _userService.GetCurrentUser();
            transaction.UserID = currentUser.Id;
            transaction.Total = _transactionService.CalculateTransactionTotal(transaction);
            bool createdTransaction = _transactionService.Create(transaction);
            if (createdTransaction)
            {
                return RedirectToAction("Index", "Transaction");
            }
            return View(transaction);
        }
    }
}
