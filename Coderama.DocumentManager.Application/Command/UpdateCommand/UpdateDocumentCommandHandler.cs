using System.Text.Json;
using Coderama.DocumentManager.Domain;
using Coderama.DocumentManager.Domain.Entity;
using Coderama.DocumentManager.Domain.Repository;
using MediatR;

namespace Coderama.DocumentManager.Application.Command.UpdateCommand;

public class UpdateDocumentCommandHandler(IUnitOfWork unitOfWork, IDocumentRepository repository)
    : IRequestHandler<UpdateDocumentCommand>
{
    public async Task Handle(
        UpdateDocumentCommand request,
        CancellationToken cancellationToken)
    {
        var existingDocument = await repository.GetDocumentByIdAsync(request.Id);
        if (existingDocument == null)
        {
            throw new Exception($"No Document could be found with ID: {request.Id}");
        }

        existingDocument.UpdateData(request.Data);
        existingDocument.UpdateTags(request.Tags.Select(Tag.Create).ToList());
        await repository.UpdateDocumentAsync(existingDocument);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}