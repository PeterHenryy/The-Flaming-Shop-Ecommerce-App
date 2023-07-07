using System.Collections.Generic;
using System.Linq;
using EcommerceApp1.Data;
using EcommerceApp1.Models.IRepositories;

namespace EcommerceApp1.Models.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Create(Company company)
        {
            try
            {
                _context.Companies.Add(company);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }

        public bool Delete(int companyID)
        {
            try
            {
                var company = GetCompanyByID(companyID);
                _context.Companies.Remove(company);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            var companies = _context.Companies.ToList();
            return companies;
        }

        public Company GetCompanyByID(int companyID)
        {
            var company = GetAllCompanies().SingleOrDefault(x => x.ID == companyID);
            return company;
        }

        public bool Update(Company company)
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
