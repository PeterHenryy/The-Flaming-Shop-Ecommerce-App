using System.Security.Claims;

namespace EcommerceApp1.Services
{
    public interface IUserService
    {
        ClaimsPrincipal GetUser();
    }
}
