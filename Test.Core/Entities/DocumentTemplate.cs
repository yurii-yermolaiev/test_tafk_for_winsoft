using System.ComponentModel.DataAnnotations;
using Test.Core.Enums;

namespace Test.Core.Entities
{
    public class DocumentTemplate
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public List<Field> LongFields { get; set; }

        public List<Field> DateFields { get; set; }

        public List<Field> StringFields { get; set; }
    }
}
