using Coderama.DocumentManager.Domain;
using Coderama.DocumentManager.Domain.Repository;
using MediatR;

namespace Coderama.DocumentManager.Application.Command.UpdateCommand;

public class UpdateDocumentCommandHandler : IRequestHandler<UpdateDocumentCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDocumentRepository _repository;

    public UpdateDocumentCommandHandler(IUnitOfWork unitOfWork, IDocumentRepository repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }
    
    public async Task Handle(
        UpdateDocumentCommand request,
        CancellationToken cancellationToken)
    {
        var existingDocument = await _repository.GetDocumentByIdASync(request.Id);
        if (existingDocument == null)
        {
            throw new Exception($"No Document could be found with ID: {request.Id}");
        }

        existingDocument.Update(request.Tags, request.Data);
        await _repository.UpdateDocumentAsync(existingDocument);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}