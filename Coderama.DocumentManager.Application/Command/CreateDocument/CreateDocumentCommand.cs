using MediatR;

namespace Coderama.DocumentManager.Application.Command.CreateDocument;

public record CreateDocumentCommand(Guid Id, List<string> Tags, object Data): IRequest;