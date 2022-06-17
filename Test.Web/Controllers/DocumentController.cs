using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost("document-template")]
        public async Task<ActionResult> CreateTemplateAsync([FromBody] CreateDocumentTemplateModel model)
        {
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
        public async Task<ActionResult> CreaTemplatesAsync([FromBody] CreateDocumentModel model)
        {
            var result = await _documentService.CreateDocumentAsync(model);

            if(!result)
            {
                 return BadRequest("Invalid data");
            }

            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPatch("documents/{id}/{status}")]
        public async Task<ActionResult> CreaDocumentAsync([FromRoute] long id, Status status)
        {
            await _documentService.ChangeDocumentStatusAsync(status, id);

            return Ok();
        }

    }
}
