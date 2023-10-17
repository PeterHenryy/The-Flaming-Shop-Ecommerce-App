using EcommerceApp1.Models.Repositories;
using EcommerceApp1.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EcommerceApp1.Models.ViewModels;
using System;
using EcommerceApp1.Helpers;

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

        public void CalculateTransactionTotal(double cartTotal, Transaction currentTransaction, IEnumerable<CartItem> cartItems)
        {
            currentTransaction.Total = cartTotal;
            ValidateCoupon(currentTransaction, cartItems, currentTransaction.CouponCode);
            currentTransaction.Total += CalculateTransactionTax(cartTotal);
            foreach (var item in cartItems)
            {
                currentTransaction.Total += item.ShippingCost;
            }
            currentTransaction.Total = Math.Round((currentTransaction.Total * 100)) / 100;
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

        public bool UpdateCompanyProperties(int? companyID, double productTotal, int quantityBought)
        {
            Company company = _transactionRepos.GetCompanyByID(companyID);
            company.Revenue += productTotal;
            company.ProductsInStock -= quantityBought;
            company.TotalSales += quantityBought;
            bool updatedCompany = _transactionRepos.UpdateCompany(company);
            return updatedCompany;
        }

        public void CreateTransactionItem(IEnumerable<CartItem> cartItems, int transactionID)
        {
            for (int i = 0; i < cartItems.Count(); i++)
            {
                CartItem cartItem = cartItems.ElementAt(i);
                TransactionItem transactionItem = new TransactionItem
                {
                    Quantity = cartItem.Quantity,
                    TransactionID = transactionID,
                    ProductID = cartItem.ProductID,
                    ShippingCost = cartItem.ShippingCost
                };
                bool createdItem = _transactionRepos.CreateTransactionItem(transactionItem);
            }
        }

        public double CalculateTransactionTax(double cartItemsTotal)
        {
            double taxPercentage = 8;
            double tax = Math.Round((cartItemsTotal * (taxPercentage / 100)) * 100) / 100;
            return tax;
        }

        public CouponValidator ValidateCoupon(Transaction transaction, IEnumerable<CartItem> cartItems, string couponCode)
        {
            CouponValidator couponValidator = new CouponValidator();
            
            foreach (var item in cartItems)
            {
                Coupon coupon = GetCoupon(couponCode, item.Product.CompanyID);
                bool isCouponValid = couponValidator.Validate(coupon, item.Product);
                if (isCouponValid)
                {
                    transaction.Total -= (transaction.Total * (coupon.DiscountPercentage / 100)) * 100 / 100;
                    couponValidator.CouponValid = isCouponValid;
                    couponValidator.CouponPercentage = coupon.DiscountPercentage;
                    break;
                }
            }
            couponValidator.Total = transaction.Total;
            return couponValidator;
        }
    }
}
