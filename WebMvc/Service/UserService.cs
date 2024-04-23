using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace WebMvc.Service
{
    public class UserService : IUserService
    {
         private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;

        public UserService(UserManager<IdentityUser> userManager, IUserStore<IdentityUser> userStore)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = (IUserEmailStore<IdentityUser>) userStore;
        }

        public async Task<string> GetUserEmail(HttpContext context)
        {
            var user = await _userManager.GetUserAsync(context.User) ?? throw new Exception("Can't find User using Claims Principal.");
            return await _userManager.GetEmailAsync(user) ?? throw new Exception("Can't find current user's email.");
        }

        public async Task UpdateAccountActivation(string email, bool isActivated)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null) return;
            IList<Claim> userClaims = await _userManager.GetClaimsAsync(user);
            var previousClaim = userClaims.Single(claim => claim.Type == "Activated");
            var newClaim = new Claim("Activated", isActivated.ToString());
            await _userManager.ReplaceClaimAsync(user, previousClaim, newClaim);
        }
    }
}