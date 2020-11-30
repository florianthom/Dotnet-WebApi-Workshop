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
            return await _dataContext.Documents.AsNoTracking().Include(a => a.Tags).ToListAsync();
        }

        public async Task<Document> GetDocumentByIdAsync(Guid documentId)
        {
            return await _dataContext.Documents.AsNoTracking().SingleOrDefaultAsync(a => a.Id == documentId);
        }

        public Task<bool> CreateDocumentAsync(Document document)
        {
            
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