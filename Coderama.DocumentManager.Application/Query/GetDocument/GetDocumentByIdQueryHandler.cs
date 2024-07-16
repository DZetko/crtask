using Coderama.DocumentManager.Domain;
using Coderama.DocumentManager.Domain.Entity;
using MediatR;

namespace Coderama.DocumentManager.Application.Query.GetDocument;

public class GetDocumentByIdQueryHandler(IDocumentRepository repository)
    : IRequestHandler<GetDocumentByIdQuery, Document?>
{
    public async Task<Document?> Handle(
        GetDocumentByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await repository.GetDocumentByIdAsync(request.Id);
    }
}