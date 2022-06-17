using Test.Core.Enums;

namespace Test.Models
{

    public class DocumentModel
    {
        public long Id { get; set; }

        public Status Status { get; set; }

        public List<LongFieldModel> LongFields { get; set; }

        public List<DateFieldModel> DateFields { get; set; }

        public List<StringFieldModel> StringFields { get; set; }

        public long TemplateId { get; set; }

        public long ApplicationUserId { get; set; }
    }
}