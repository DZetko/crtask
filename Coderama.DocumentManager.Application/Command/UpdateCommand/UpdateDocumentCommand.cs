using MediatR;

namespace Coderama.DocumentManager.Application.Command.UpdateCommand;

public record UpdateDocumentCommand(Guid Id, List<string> Tags, object Data) : IRequest;