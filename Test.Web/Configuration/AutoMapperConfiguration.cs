using AutoMapper;
using Test.Core.Entities;
using Test.Models;

namespace Test.Web.Configuration
{
    public static class AutoMapperConfiguration
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AuthModel, ApplicationUser>();

            CreateMap<Document, DocumentModel>();
            CreateMap<DocumentModel, Document>();

            CreateMap<DateFieldModel, DateField>();
            CreateMap<DateField, DateFieldModel>();

            CreateMap<StringFieldModel, StringField>();
            CreateMap<StringField, StringFieldModel>();

            CreateMap<LongFieldModel, LongField>();
            CreateMap<LongField, LongFieldModel>();

            CreateMap<DateFieldModel, Field>();
            CreateMap<Field, DateFieldModel>();

            CreateMap<StringFieldModel, Field>();
            CreateMap<Field, StringFieldModel>();

            CreateMap<LongFieldModel, Field>();
            CreateMap<Field, LongFieldModel>();

            CreateMap<DocumentTemplate, DocumentTemplateModel>();

            CreateMap<CreateDocumentTemplateModel, DocumentTemplate>();

            CreateMap<CreateDocumentModel, Document>();

            CreateMap<FieldModel, Field>();
        }
    }
}
