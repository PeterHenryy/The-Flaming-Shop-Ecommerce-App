using EcommerceApp1.Models;
using EcommerceApp1.Models.Identity;
using EcommerceApp1.Models.ViewModels;
using EcommerceApp1.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EcommerceApp1.Controllers
{
    public class CompanyController : Controller
    {
        private readonly CompanyService _companyService;
        private readonly UserService _userService;

        public CompanyController(CompanyService companyService, UserService userService)
        {
            _companyService = companyService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var companies = _companyService.GetAllCompanies();
            return View(companies);
        }

        public IActionResult CompanyStats()
        {
            AppUser user = _userService.GetCurrentUser();
            Company userCompany = _companyService.GetCompanyByID(user.CompanyID);
            var companyStatsViewModel = new CompanyStatsViewModel();
            companyStatsViewModel.Company = userCompany;
            companyStatsViewModel.YearCustomers = _companyService.GetCompanyYearCustomers(userCompany.ID);
            companyStatsViewModel.YearOrders = _companyService.GetCompanyYearOrders(userCompany.ID);
            companyStatsViewModel.YearRevenue = _companyService.GetCompanyYearRevenue(userCompany.ID);
            companyStatsViewModel.CompanyProductsReviews = _companyService.GetCompanyLatestReviews(userCompany.ID);
            companyStatsViewModel.CompanyPurchasedItems = _companyService.GetCompanyLatestPurchases(userCompany.ID);
            return View(companyStatsViewModel);
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

        [HttpGet]
        public IActionResult GetCompanyRevenuesPerMonth()
        {
            AppUser user = _userService.GetCurrentUser();
            var revenues = _companyService.GetCompanyRevenuesPerMonth(user.CompanyID);
            return Json(revenues);
        }
    }
}
