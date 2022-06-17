using System.ComponentModel.DataAnnotations;

namespace Test.Core.Entities
{
    public class DateField : Field
    {
        public DateTime Value { get; set; }
    }
}
