using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class DateFieldModel : FieldModel
    {
        [Required]
        public DateTime Value { get; set; }
    }
}
