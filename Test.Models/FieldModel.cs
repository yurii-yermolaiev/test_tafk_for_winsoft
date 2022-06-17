using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class FieldModel 
    {
        [Required]
        public string Name { get; set; }
    }
}
