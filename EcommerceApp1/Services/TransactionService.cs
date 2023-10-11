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

        public double CalculateTransactionTotal(double cartTotal, double discountPercentage = 0)
        {
            return (discountPercentage != 0) ? cartTotal -= (discountPercentage / 100) * cartTotal : cartTotal;
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

        public bool UpdateProductStock(int? productID, int quantity)
        {
            Product product = _transactionRepos.GetProductByID(productID);
            product.Stock -= quantity;
            bool updatedProduct = _transactionRepos.UpdateProductStock(product);
            return updatedProduct;
        }

        public bool UpdateCompanyProperties(int? companyID, double transactionTotal, int quantityBought)
        {
            Company company = _transactionRepos.GetCompanyByID(companyID);
            company.Revenue += transactionTotal;
            company.ProductsInStock -= quantityBought;
            company.TotalSales += quantityBought;
            bool updatedCompany = _transactionRepos.UpdateCompany(company);
            return updatedCompany;
        }
    }
}
