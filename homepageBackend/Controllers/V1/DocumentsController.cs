using System;
using System.Threading.Tasks;
using AutoMapper;
using homepageBackend.Cache;
using homepageBackend.Contracts.V1;
using homepageBackend.Contracts.V1.Responses;
using homepageBackend.Domain;
using homepageBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace homepageBackend.Controllers
{
    public class DocumentController : Controller
    {
        private readonly DocumentService _documentService;
        private readonly UriService _uriService;
        private readonly IMapper _mapper;

        public DocumentController(DocumentService documentService, UriService uriService, IMapper mapper)
        {
            _documentService = documentService;
            _uriService = uriService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route(ApiRoutes.Documents.GetAll)]
        [Cache(600)]
        public async Task<IActionResult> GetAll()
        {
            var documents = await _documentService.GetDocumentsAsync();
            return Ok(documents);
        }

        [HttpGet]
        [Route(ApiRoutes.Documents.Get)]
        [Cache(600)]
        public async Task<IActionResult> Get([FromRoute] Guid documentId)
        {
            var document = await _documentService.GetDocumentByIdAsync(documentId);

            if (document==null)
                return NotFound();
            
            return Ok(new Response<DocumentResponse>(_mapper.Map<DocumentResponse>(document)));
        }
    }
}