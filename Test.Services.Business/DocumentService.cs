using AutoMapper;
using Test.Core.Entities;
using Test.Core.Enums;
using Test.Models;
using Test.Repositories.Interfaces;
using Test.Services.Interfacess;

namespace Test.Services.Business
{
    public class DocumentService : IDocumentService
    {
        private readonly IRepository<Document> _documentReposetory;

        private readonly IRepository<DocumentTemplate> _documentTemplateReposetory;

        private readonly IMapper _mapper;

        private readonly IUserService _userService;

        public DocumentService(IRepository<Document> documentReposetory,
            IRepository<DocumentTemplate> documentTemplateReposetory,
            IMapper mapper,
            IUserService userService)
        {
            _documentReposetory = documentReposetory;
            _mapper = mapper;
            _documentTemplateReposetory = documentTemplateReposetory;
            _userService = userService;
        }
        public async Task ChangeDocumentStatusAsync(Status status, long id)
        {
            var document = await _documentReposetory.GetAsync(id);
            
            if(document != null)
            {
                document.Status = status;

                await _documentReposetory.UpdateAsync(document);

                await _documentReposetory.SaveAsync();
            };
        }

        public async Task<bool> CreateDocumentAsync(CreateDocumentModel model)
        {
            if (!await ValidateDocumentAsync(model))
            {
                return false;
            }

            var user = await _userService.GetCurrentUserAsync();

            var document = _mapper.Map<Document>(model);

            document.ApplictionUser = user;

            await _documentReposetory.CreateAsync(document);

            await _documentReposetory.SaveAsync();

            return true;
        }

        public async Task CreateTemplateAsync(CreateDocumentTemplateModel model)
        {
            var template = _mapper.Map<DocumentTemplate>(model);

            await _documentTemplateReposetory.CreateAsync(template);

            await _documentTemplateReposetory.SaveAsync();
        }

        public async Task<DocumentTemplateModel> GetTemplateModelAsync(long id)
        {
            var template = await _documentTemplateReposetory.GetAsync(id);

            if(template == null)
            {
                return null;
            }    

            var result = _mapper.Map<DocumentTemplateModel>(template);

            return result;
        }

        public async Task<IEnumerable<DocumentTemplateModel>> GetTemplateModelsAsync()
        {
            var templates = await _documentTemplateReposetory.GetAllAsync();

            var result = _mapper.Map<List<DocumentTemplateModel>>(templates);

            return result;
        }

        private async Task<bool> ValidateDocumentAsync(CreateDocumentModel model)
        {
            var template = await _documentTemplateReposetory.GetAsync(model.TemplateId);

            if(template == null)
            {
                return false;
            }

            foreach(var stringField in template.StringFields)
            {
                var check = model.StringFields.Any(sf => sf.Name == stringField.Name);

                if(!check)
                {
                    return false;
                }
            }

            foreach (var dateField in template.DateFields)
            {
                var check = model.DateFields.Any(sf => sf.Name == dateField.Name);

                if (!check)
                {
                    return false;
                }
            }

            foreach (var longField in template.LongFields)
            {
                var check = model.LongFields.Any(sf => sf.Name == longField.Name);

                if (!check)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
