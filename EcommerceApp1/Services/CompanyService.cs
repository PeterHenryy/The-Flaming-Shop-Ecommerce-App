using EcommerceApp1.Models;
using EcommerceApp1.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EcommerceApp1.Services
{
    public class CompanyService
    {
        private readonly CompanyRepository _companiesRepo;

        public CompanyService(CompanyRepository companiesRepo)
        {
            _companiesRepo = companiesRepo;
        }

        public bool Create(Company company)
        {
            var createdCompany = _companiesRepo.Create(company);
            return createdCompany;
        }

        public bool Delete(int companyID)
        {
            var deletedCompany = _companiesRepo.Delete(companyID);
            return deletedCompany;
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            var companies = _companiesRepo.GetAllCompanies();
            return companies;
        }

        public Company GetCompanyByID(int companyID)
        {
            var company = _companiesRepo.GetCompanyByID(companyID);
            return company;
        }

        public bool Update(Company company)
        {
            var updatedCompany = _companiesRepo.Update(company);
            return updatedCompany;
        }
    }
}
