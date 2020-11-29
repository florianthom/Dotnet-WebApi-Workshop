using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using homepageBackend.Cache;
using homepageBackend.Contracts.V1;
using homepageBackend.Contracts.V1.Requests;
using homepageBackend.Contracts.V1.Responses;
using homepageBackend.Data.Migrations;
using homepageBackend.Domain;
using homepageBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace homepageBackend.Controllers
{
    public class DocumentController : Controller
    {
        private readonly IDocumentService _documentService;
        private readonly IUriService _uriService;
        private readonly IMapper _mapper;

        public DocumentController(IDocumentService documentService, IUriService uriService, IMapper mapper)
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
            return Ok(new Response<List<DocumentResponse>>(_mapper.Map<List<DocumentResponse>>(documents)));
        }

        [HttpGet]
        [Route(ApiRoutes.Documents.Get)]
        [Cache(600)]
        public async Task<IActionResult> Get([FromRoute] Guid documentId)
        {
            var document = await _documentService.GetDocumentByIdAsync(documentId);

            if (document == null)
                return NotFound();

            return Ok(new Response<DocumentResponse>(_mapper.Map<DocumentResponse>(document)));
        }

        public async Task<IActionResult> Create([FromBody] CreateDocumentRequest documentRequest)
        {
            return null;
        }
    }
}