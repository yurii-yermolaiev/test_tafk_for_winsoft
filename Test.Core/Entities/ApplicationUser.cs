using Microsoft.AspNetCore.Identity;

namespace Test.Core.Entities
{
    public class ApplicationUser : IdentityUser<long>
    {
        public List<Document> Documents { get; set; }
    }
}