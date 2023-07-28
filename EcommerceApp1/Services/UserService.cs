using EcommerceApp1.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
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

        public double CalculateUserRewardPoints(double productPrice)
        {
            double rewardPoints = productPrice / 2;
            return rewardPoints;
        }

        public AppUser MapUserUpdates(AppUser updatedUser, AppUser currentUser)
        {
            if (!String.IsNullOrEmpty(updatedUser.FirstName))
            {
                currentUser.FirstName = updatedUser.FirstName;
            }
            if (!String.IsNullOrEmpty(updatedUser.LastName))
            {
                currentUser.LastName = updatedUser.LastName;
            }
            if (!String.IsNullOrEmpty(updatedUser.Password))
            {
                currentUser.Password = updatedUser.Password;
            }
            if (!String.IsNullOrEmpty(updatedUser.ProfilePicture))
            {
                currentUser.ProfilePicture = updatedUser.ProfilePicture;
            }
            if (!String.IsNullOrEmpty(updatedUser.Email))
            {
                currentUser.Email = updatedUser.Email;
            }
            if (!String.IsNullOrEmpty(updatedUser.PhoneNumber))
            {
                currentUser.PhoneNumber = updatedUser.PhoneNumber;
            }
            if (!String.IsNullOrEmpty(updatedUser.UserName))
            {
                currentUser.UserName = updatedUser.UserName;
            }
            return currentUser;
        }
    }
}
