using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class CreateDocumentTemplateModel
    {
        [Required]
        public string Name { get; set; }

        public List<FieldModel> LongFields { get; set; }

        public List<FieldModel> DateFields { get; set; }

        public List<FieldModel> StringFields { get; set; }
    }
}
