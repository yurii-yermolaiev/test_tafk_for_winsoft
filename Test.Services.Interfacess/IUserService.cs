using Test.Core.Entities;

namespace Test.Services.Interfacess
{
    public interface IUserService
    {
        Task<ApplicationUser> GetCurrentUserAsync();
    }
}
