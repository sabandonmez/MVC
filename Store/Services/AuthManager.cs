using Microsoft.AspNetCore.Identity;
using Services.Contracts;

namespace Services
{
    public class AuthManager : IAuthService
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public AuthManager(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IEnumerable<IdentityRole> Roles => roleManager.Roles;

        public IEnumerable<IdentityUser> GetAllUser()
        {
            return userManager.Users.ToList();
        }
    }
}