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
            return await _dataContext.Documents.SingleOrDefaultAsync(a => a.Id == documentId);
        }

        public async Task<bool> CreateDocumentAsync(Document document)
        {
            document.Tags.ForEach(a => a.TagName = a.TagName.ToString());
            await AddNewTag(document);
            await _dataContext.Documents.AddAsync(document);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        private async Task AddNewTag(Document document)
        {

                foreach (var tag in document.Tags)
                {
                    var existingTag =
                        await _dataContext.Tags.SingleOrDefaultAsync(x =>
                            x.Name == tag.TagName);
                    if (existingTag != null)
                        continue;

                    await _dataContext.Tags.AddAsync(new Tag
                        {Name = tag.TagName, CreatedOn = DateTime.UtcNow, CreatorId = document.UserId});
                }
        }

        public async Task<bool> UpdateDocumentAsync(Document documentToUpdate)
        {
            documentToUpdate.Tags?.ForEach(a => a.TagName = a.TagName.ToLower());
            await AddNewTags(documentToUpdate);
            _dataContext.Documents.Update(documentToUpdate);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        private async Task AddNewTags(Document document)
        {
            foreach (var tag in document.Tags)
            {
                var existing_tag = await _dataContext.Tags.SingleOrDefaultAsync(a => a.Name == tag.TagName);
                if (existing_tag!=null)
                {
                    continue;
                }
                
                await _dataContext.Tags.AddAsync(new Tag()
                {
                    Name = tag.TagName,
                    CreatedOn = DateTime.UtcNow,
                    CreatorId = document.UserId
                });
            }
        }

        public async Task<bool> DeleteDocumentAsync(Guid documentId)
        {
            Document document = await GetDocumentByIdAsync(documentId);
            if (document==null)
            {
                return false;
            }
            
            _dataContext.Documents.Remove(document);
            var removed = await _dataContext.SaveChangesAsync();
            return removed > 0;
        }

        public async Task<bool> UserOwnsDocumentAsync(Guid documentId, string userId)
        {
            var project = await _dataContext.Projects.AsNoTracking().SingleOrDefaultAsync(a => a.Id == documentId);

            if (project == null)
            {
                return false;
            }

            if (project.UserId != userId)
            {
                return false;
            }

            return true;
        }
    }
}