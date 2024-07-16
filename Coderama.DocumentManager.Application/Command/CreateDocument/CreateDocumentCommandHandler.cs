using System.Text.Json;
using Coderama.DocumentManager.Domain;
using Coderama.DocumentManager.Domain.Entity;
using Coderama.DocumentManager.Domain.Repository;
using MediatR;

namespace Coderama.DocumentManager.Application.Command.CreateDocument;

public class CreateDocumentCommandHandler(IUnitOfWork unitOfWork, IDocumentRepository repository)
    : IRequestHandler<CreateDocumentCommand>
{
    public async Task Handle(
        CreateDocumentCommand request,
        CancellationToken cancellationToken)
    {
        var document = Document.Create(request.Id, request.Tags.Select(Tag.Create).ToList(), request.Data);
        await repository.CreateDocumentAsync(document);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}