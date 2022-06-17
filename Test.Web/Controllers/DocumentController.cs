using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Test.Core.Enums;
using Test.Models;
using Test.Services.Interfacess;

namespace Test.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        private readonly ILogger<DocumentController> _logger;

        public DocumentController(IDocumentService documentService,
            ILogger<DocumentController> logger)
        {
            _documentService = documentService;
            _logger = logger;
        }

        [HttpPost("document-template")]
        public async Task<ActionResult> CreateTemplateAsync([FromBody] CreateDocumentTemplateModel model)
        {
            _logger.LogInformation($"method: {nameof(CreateTemplateAsync)}, body : {JsonConvert.SerializeObject(model)}");

            await _documentService.CreateTemplateAsync(model);

            return Ok();
        }

        [HttpGet("document-templates")]
        public async Task<ActionResult<IEnumerable<DocumentTemplateModel>>> GetDocumentTemplatesAsync()
        {
            var result = await _documentService.GetDocumentTemplatesAsync();

            return Ok(result);
        }

        [HttpGet("documents")]
        public async Task<ActionResult<IEnumerable<DocumentModel>>> GetDocumentsAsync()
        {
            var result = await _documentService.GetDocumentsAsync();

            return Ok(result);
        }

        [HttpGet("document-templates/{id}")]
        public async Task<ActionResult<DocumentTemplateModel>> GetTemplatesAsync([FromRoute] long id)
        {
            var result = await _documentService.GetTemplateAsync(id);

            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("document")]
        public async Task<ActionResult> CreateDocumentAsync([FromBody] CreateDocumentModel model)
        {
            _logger.LogInformation($"method: {nameof(CreateDocumentAsync)}, body : {JsonConvert.SerializeObject(model)}");

            var result = await _documentService.CreateDocumentAsync(model);

            if(!result)
            {
                _logger.LogError($"method: {nameof(CreateDocumentAsync)}, error : \"Invalid data\"");
                 return BadRequest("Invalid data");
            }

            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPatch("documents/{id}/{status}")]
        public async Task<ActionResult> CreaDocumentAsync([FromRoute] long id, Status status)
        {
            _logger.LogInformation($"method: {nameof(CreaDocumentAsync)}, id : {id}, status : {status}");

            await _documentService.ChangeDocumentStatusAsync(status, id);

            return Ok();
        }

    }
}
