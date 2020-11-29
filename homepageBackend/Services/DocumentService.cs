using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using homepageBackend.Data;
using homepageBackend.Domain;
using Microsoft.EntityFrameworkCore;

namespace homepageBackend.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly DataContext _dataContext;

        public DocumentService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Document>> GetDocumentsAsync()
        { 
            return await _dataContext.Documents.Include(a => a.Tags).ToListAsync();
        }

        public Task<Document> GetDocumentByIdAsync(Guid documentId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateDocumentAsync(Document document)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDocumentAsync(Document documentToUpdate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDocumentAsync(Guid documentId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserOwnsDocumentAsync(Guid documentId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}