using System.ComponentModel.DataAnnotations;
using Test.Core.Enums;

namespace Test.Models
{
    
    public class CreateDocumentModel
    {
        public List<LongFieldModel> LongFields { get; set; }

        public List<DateFieldModel> DateFields { get; set; }

        public List<StringFieldModel> StringFields { get; set; }

        [Required]
        public long TemplateId { get; set; }
    }
}