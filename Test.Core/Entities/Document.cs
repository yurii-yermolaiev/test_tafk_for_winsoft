using System.ComponentModel.DataAnnotations;
using Test.Core.Enums;

namespace Test.Core.Entities
{
    public class Document
    {
        [Key]
        public long Id { get; set; }

        public Status? Status { get; set; }

        public List<LongField> LongFields { get; set; }

        public List<DateField> DateFields { get; set; }

        public List<StringField> StringFields { get; set; }

        public long ApplicationUserId { get; set; }

        public ApplicationUser ApplictionUser { get; set; }

        public long TemplateId { get; set; }

        public DocumentTemplate Template { get; set; }
    }
}
