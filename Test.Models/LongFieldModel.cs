using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class LongFieldModel : FieldModel
    {
        [Required]
        public long Value { get; set; }
    }
}
