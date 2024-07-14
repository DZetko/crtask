namespace Coderama.DocumentManager.Presentation;

public class UpdateDocumentDto
{
    public Guid Id { get; init; }
    public List<string> Tags { get; init; }
    public string Data { get; init; }
}