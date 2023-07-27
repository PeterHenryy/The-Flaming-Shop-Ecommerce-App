using EcommerceApp1.Models.Repositories;
using EcommerceApp1.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EcommerceApp1.Models.ViewModels;

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

        public Product GetProductByID(int? productID)
        {
            var product = _transactionRepos.GetProductByID(productID);
            return product;
        }

        public double CalculateTransactionTotal(Transaction transaction, double discountPercentage = 0)
        {
            var total = transaction.CurrentProduct.Price * transaction.QuantityBought;
            if (discountPercentage != 0)
            {
                total -= (discountPercentage / 100) * total;
            }
            return total;
        }

        public List<CreditCard> GetSpecificUserCards(int userID)
        {
            var userCards = _transactionRepos.GetSpecificUserCards(userID);
            return userCards;
        }

        public IEnumerable<Transaction> GetTransactionsByUserID(int userID)
        {
            var userTransactions = GetAllTransactions().Where(x => x.UserID == userID);
            return userTransactions;
        }

        public IEnumerable<Refund> GetAllUserRefunds(int userID)
        {
            var refunds = _transactionRepos.GetAllRefunds().Where(x => x.UserID == userID);
            return refunds;
        }

        public Coupon GetCoupon(string code, int? companyID)
        {
            Coupon coupon = _transactionRepos.GetCoupons().SingleOrDefault(x => x.Code == code && x.CompanyID == companyID);
            return coupon;
        }

        public bool UpdateCouponQuantity(Coupon coupon)
        {
            bool updatedCoupon = _transactionRepos.UpdateCouponQuantity(coupon);
            return updatedCoupon;
        }

        public bool UpdateProductStock(int? productID)
        {
            Product product = _transactionRepos.GetProductByID(productID);
            product.Stock--;
            bool updatedProduct = _transactionRepos.UpdateProductStock(product);
            return updatedProduct;
        }
    }
}
