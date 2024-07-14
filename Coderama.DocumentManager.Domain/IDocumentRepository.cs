using Coderama.DocumentManager.Domain.Entity;

namespace Coderama.DocumentManager.Domain;

public interface IDocumentRepository
{
    Task<Document?> GetDocumentByIdASync(
        Guid id);
    
    Task CreateDocumentAsync(
        Document document);
    
    Task UpdateDocumentAsync(
        Document document);
}