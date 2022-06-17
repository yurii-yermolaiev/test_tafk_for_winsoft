using Test.Core.Entities;

namespace Test.Services.Interfacess
{
    public interface IJwtTokenService
    {
        public Task<string> GetJwtTokenWithRolesAsync(string email, ApplicationUser user);
    }
}
