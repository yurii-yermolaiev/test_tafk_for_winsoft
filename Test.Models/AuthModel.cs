using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class AuthModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
