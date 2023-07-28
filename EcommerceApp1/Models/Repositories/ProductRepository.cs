using EcommerceApp1.Data;
using EcommerceApp1.Models.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceApp1.Models.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Create(Product product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }

        public bool Delete(int productID)
        {
            try
            {
                var product = GetProductByID(productID);
                _context.Products.Remove(product);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = _context.Products.Include(x => x.Category).
                                              Include(x => x.Company).ToList();
            return products;
        }

        public Product GetProductByID(int productID)
        {
            var product = GetAllProducts().SingleOrDefault(x => x.ID == productID);
            return product;
        }

        public bool Update(Product product)
        {
            try
            {
                _context.ChangeTracker.Clear();
                _context.Products.Update(product);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }

        public List<Category> GetAllCategories()
        {
            var categories = _context.Categories.ToList();
            return categories;
        }

        public List<Company> GetAllCompanies()
        {
            var companies = _context.Companies.ToList();
            return companies;
        }

        public Company GetCompanyByID(int? companyID)
        {
            Company company = _context.Companies.SingleOrDefault(x => x.ID == companyID);
            return company;
        }

        public IEnumerable<Review> GetReviews()
        {
            var reviews = _context.Reviews.Include(x => x.User);
            return reviews;
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            var transactions = _context.Transactions.ToList();
            return transactions;
        }

        public IEnumerable<Comment> GetAllComments()
        {
            var comments = _context.Comments.Include(x => x.User).ToList();
            return comments;
        }

        public IEnumerable<Like> GetLikes()
        {
            var likes = _context.Likes.ToList();
            return likes;
        }

        public IEnumerable<Dislike> GetDislikes()
        {
            var dislikes = _context.Dislikes.ToList();
            return dislikes;
        }

        public bool UpdateCompanyProductStock(Company company)
        {
            try
            {
                _context.Companies.Update(company);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
