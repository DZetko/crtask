using Coderama.DocumentManager.Domain;
using Coderama.DocumentManager.Domain.Entity;
using MediatR;

namespace Coderama.DocumentManager.Application.Query.GetDocument;

public class GetDocumentByIdQueryHandler : IRequestHandler<GetDocumentByIdQuery, Document?>
{
    private readonly IDocumentRepository _repository;

    public GetDocumentByIdQueryHandler(IDocumentRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Document?> Handle(
        GetDocumentByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.GetDocumentByIdASync(request.Id);
    }
}