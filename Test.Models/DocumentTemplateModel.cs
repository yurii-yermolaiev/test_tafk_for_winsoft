namespace Test.Models
{
    public class DocumentTemplateModel
    {
        public long Id { get; set; }

        public List<LongFieldModel> LongFields { get; set; }

        public List<DateFieldModel> DateFields { get; set; }

        public List<StringFieldModel> StringFields { get; set; }
    }
}