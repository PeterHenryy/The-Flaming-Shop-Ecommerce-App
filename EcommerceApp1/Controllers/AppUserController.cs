using EcommerceApp1.Helpers.Enums;
using EcommerceApp1.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcommerceApp1.Controllers
{
    public class AppUserController : Controller
    {
        private SignInManager<AppUser> _signInManager; 
        private RoleManager<AppRole> _roleManager;
        private UserManager<AppUser> _userManger; 

        public AppUserController(SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager, UserManager<AppUser> userManger)
        {
            _userManger = userManger;
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
            var userRegister = await _userManger.CreateAsync(appUser);
            var assignRole = await _userManger.AddToRoleAsync(appUser, role);

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
            AppUser user = await _userManger.FindByNameAsync(appLogin.Username);
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


    }
}
