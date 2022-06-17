using Test.Core.Enums;
using Test.Models;

namespace Test.Services.Interfacess
{
    public interface IDocumentService
    {
        public Task CreateTemplateAsync(CreateDocumentTemplateModel model);

        public Task<IEnumerable<DocumentTemplateModel>> GetDocumentTemplatesAsync();

        public Task<IEnumerable<DocumentModel>> GetDocumentsAsync();

        public Task<DocumentTemplateModel> GetTemplateAsync(long id);

        public Task<bool> CreateDocumentAsync(CreateDocumentModel model);

        public Task ChangeDocumentStatusAsync(Status status, long id);
    }
}
