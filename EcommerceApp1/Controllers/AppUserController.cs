using EcommerceApp1.Helpers.Enums;
using EcommerceApp1.Models.Identity;
using EcommerceApp1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EcommerceApp1.Controllers
{
    public class AppUserController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager; 
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly UserService _userService;
        private readonly AppUser _currentUser;

        public AppUserController(SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager, UserManager<AppUser> userManger, UserService userService)
        {
            _userManager = userManger;
            _userService = userService;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _currentUser = userService.GetCurrentUser();

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<RedirectToActionResult> Register(AppUser appUser)
        {
            appUser.ProfilePicture = "user-solid.svg";
            var role = UserRolesEnum.Customer.ToString();
            var userRegister = await _userManager.CreateAsync(appUser);
            var assignRole = await _userManager.AddToRoleAsync(appUser, role);

            return RedirectToAction("Login", "AppUser");
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(AppLogin appLogin, string returnUrl)
        {
            AppUser user = await _userManager.FindByNameAsync(appLogin.Username);
            if (user.Password == appLogin.Password)
            {
                await _signInManager.SignInAsync(user, false);
                if (!String.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Product");
            }
            return View();
        }

        public async Task<RedirectToActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "AppUser");
        }

        [HttpGet]
        public IActionResult Update()
        {
            return View(_currentUser);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AppUser updatedUser)
        {
            var files = HttpContext.Request.Form.Files;
            if(files.Count > 0)
            {
                updatedUser.ProfilePicture = files[0].FileName;
                _userService.HandleUserProfilePicture(files);
            }
            updatedUser.SecurityStamp = Guid.NewGuid().ToString();
            AppUser mappedUser = await _userService.MapUserUpdates(updatedUser, _currentUser, _userManager);
            var user = await _userManager.UpdateAsync(mappedUser);
            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public IActionResult RegisterAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAdmin(AppUser admin)
        {
            admin.CompanyID = _currentUser.CompanyID;
            admin.IsAdmin = true;
            var registeredAdmin = await _userManager.CreateAsync(admin);
            var role = UserRolesEnum.Admin.ToString();
            var assignedRole = await _userManager.AddToRoleAsync(admin, role);
            return RedirectToAction("CompanyStats", "Company");
        }
        

    }
}
