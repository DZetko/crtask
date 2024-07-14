using MediatR;

namespace Coderama.DocumentManager.Application.Command.UpdateCommand;

public record UpdateDocumentCommand(Guid Id, List<string> Tags, string Data) : IRequest;