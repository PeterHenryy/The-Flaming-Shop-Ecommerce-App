using EcommerceApp1.Models.Repositories;
using EcommerceApp1.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceApp1.Services
{
    public class TransactionService
    {
        private readonly TransactionRepository _transactionRepos;

        public TransactionService(TransactionRepository transactionRepos)
        {
            _transactionRepos = transactionRepos;
        }

        public bool Create(Transaction transaction)
        {
            var createdTransaction = _transactionRepos.Create(transaction);
            return createdTransaction;
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            var transactions = _transactionRepos.GetAllTransactions();
            return transactions;
        }

        public Transaction GetTransactionByID(int transactionID)
        {
            var transaction = GetAllTransactions().SingleOrDefault(x => x.ID == transactionID);
            return transaction;
        }

        public Product GetProductByID(int productID)
        {
            var product = _transactionRepos.GetProductByID(productID);
            return product;
        }

        public double CalculateTransactionTotal(Transaction transaction)
        {
            var total = transaction.CurrentProduct.Price * transaction.QuantityBought;
            return total;
        }

    }
}
