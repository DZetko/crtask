using Coderama.DocumentManager.Domain.Entity;
using MediatR;

namespace Coderama.DocumentManager.Application.Query.GetDocument;

public record GetDocumentByIdQuery(Guid Id) : IRequest<Document?>;