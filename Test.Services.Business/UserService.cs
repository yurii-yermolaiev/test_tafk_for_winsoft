using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Test.Core.Entities;

namespace Test.Services.Interfacess
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IHttpContextAccessor context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetCurrentUserAsync()
        {
            var userName = _context.HttpContext?.User?.Identity?.Name;
            if(string.IsNullOrEmpty(userName))
            {
                return null;
            }    
            var user = await _userManager.FindByNameAsync(userName);
            return user;
        }
    }
}
