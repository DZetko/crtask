using Coderama.DocumentManager.Domain;
using Coderama.DocumentManager.Domain.Entity;
using Coderama.DocumentManager.Domain.Repository;
using MediatR;

namespace Coderama.DocumentManager.Application.Command.CreateDocument;

public class CreateDocumentCommandHandler: IRequestHandler<CreateDocumentCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDocumentRepository _repository;

    public CreateDocumentCommandHandler(IUnitOfWork unitOfWork, IDocumentRepository repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }
    
    public async Task Handle(
        CreateDocumentCommand request,
        CancellationToken cancellationToken)
    {
        var document = Document.Create(request.Id, request.Tags, request.Data);
        await _repository.CreateDocumentAsync(document);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}