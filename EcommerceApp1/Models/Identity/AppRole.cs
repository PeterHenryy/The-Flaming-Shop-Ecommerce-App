using Microsoft.AspNetCore.Identity;

namespace EcommerceApp1.Models.Identity
{
    public class AppRole : IdentityRole<int>
    {
        public string Description { get; set; }
    }
}
