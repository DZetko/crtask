using Coderama.DocumentManager.Domain;
using Coderama.DocumentManager.Domain.Entity;
using MediatR;

namespace Coderama.DocumentManager.Application.Command.CreateDocument;

public record CreateDocumentCommand(Guid Id, List<string> Tags, string Data): IRequest;