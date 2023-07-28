using EcommerceApp1.Helpers.Enums;
using EcommerceApp1.Models.Identity;
using EcommerceApp1.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EcommerceApp1.Controllers
{
    public class AppUserController : Controller
    {
        private SignInManager<AppUser> _signInManager; 
        private RoleManager<AppRole> _roleManager;
        private UserManager<AppUser> _userManager;
        private readonly UserService _userService;

        public AppUserController(SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager, UserManager<AppUser> userManger, UserService userService)
        {
            _userManager = userManger;
            _userService = userService;
            _signInManager = signInManager;
            _roleManager = roleManager;

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

            var role = UserRolesEnum.Customer.ToString();
            var userRegister = await _userManager.CreateAsync(appUser);
            var assignRole = await _userManager.AddToRoleAsync(appUser, role);

            return RedirectToAction("Login", "AppUser");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AppLogin appLogin)
        {
            AppUser user = await _userManager.FindByNameAsync(appLogin.Username);
            if (user.Password == appLogin.Password)
            {
                await _signInManager.SignInAsync(user, false);
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
            AppUser user = _userService.GetCurrentUser();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AppUser updatedUser)
        {
            updatedUser.SecurityStamp = Guid.NewGuid().ToString();
            AppUser currentUser = _userService.GetCurrentUser();
            AppUser mappedUser = await _userService.MapUserUpdates(updatedUser, currentUser, _userManager);
            var user = await _userManager.UpdateAsync(mappedUser);
            return RedirectToAction("Index", "Product");
        }

        

    }
}
