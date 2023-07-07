using EcommerceApp1.Models;
using EcommerceApp1.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp1.Controllers
{
    public class CompanyController : Controller
    {
        private readonly CompanyService _companyService;

        public CompanyController(CompanyService companyService)
        {
            _companyService = companyService;
        }

        public IActionResult Index()
        {
            var companies = _companyService.GetAllCompanies();
            return View(companies);
        }

        [HttpGet]
        public IActionResult Update(int companyID)
        {
            var company = _companyService.GetCompanyByID(companyID);
            return View(company);
        }

        [HttpPost]
        public IActionResult Update(Company company)
        {
            var updatedCompany = _companyService.Update(company);
            if (updatedCompany)
            {
                return RedirectToAction("Index", "Company");
            }
            return View(company);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Company company)
        {
            var updatedCompany = _companyService.Create(company);
            if (updatedCompany)
            {
                return RedirectToAction("Index", "Company");
            }
            return View();
        }

        public IActionResult Delete(int companyID)
        {
            _companyService.Delete(companyID);
            return RedirectToAction("Index", "Company");
        }

    }
}
