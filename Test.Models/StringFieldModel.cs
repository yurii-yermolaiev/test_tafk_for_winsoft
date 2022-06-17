using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class StringFieldModel : FieldModel
    {
        [Required]
        public string Value { get; set; }
    }
}
