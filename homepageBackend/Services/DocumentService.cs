using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using homepageBackend.Domain;

namespace homepageBackend.Services
{
    public class DocumentService : IDocumentService
    {
        public Task<List<Document>> GetDocumentsAsync()
        {
            throw new NotImplementedException();
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