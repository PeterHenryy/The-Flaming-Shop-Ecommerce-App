using EcommerceApp1.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EcommerceApp1.Services
{

    public class UserService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<AppUser> _userManager;

        public UserService(IHttpContextAccessor httpContext, UserManager<AppUser> userManager)
        {
            _httpContext = httpContext;
            _userManager = userManager;
        }

        public AppUser GetCurrentUser()
        {
            var userID = _userManager.GetUserId(_httpContext.HttpContext.User);
            var user = _userManager.FindByIdAsync(userID).Result;
            return user;
        }
    }
}
