using EcommerceApp1.Data;
using EcommerceApp1.Models.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceApp1.Models.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Create(Transaction transaction)
        {
            try
            {
                _context.Transactions.Add(transaction);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            var transactions = _context.Transactions.ToList();
            return transactions;
        }

        public Transaction GetTransactionByID(int transactionID)
        {
            var transaction = GetAllTransactions().SingleOrDefault(x => x.ID == transactionID);
            return transaction;
        }

        public Product GetProductByID(int productID)
        {
            var product = _context.Products.SingleOrDefault(x => x.ID == productID);
            return product;
        }
    }
}
