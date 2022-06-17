using System.ComponentModel.DataAnnotations;

namespace Test.Core.Entities
{
    public class Field
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }
    }
}
