using Test.Core.Enums;
using Test.Models;

namespace Test.Services.Interfacess
{
    public interface IDocumentService
    {
        public Task CreateTemplateAsync(CreateDocumentTemplateModel model);

        public Task<IEnumerable<DocumentTemplateModel>> GetTemplateModelsAsync();

        public Task<DocumentTemplateModel> GetTemplateModelAsync(long id);

        public Task<bool> CreateDocumentAsync(CreateDocumentModel model);

        public Task ChangeDocumentStatusAsync(Status status, long id);
    }
}
