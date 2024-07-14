namespace Coderama.DocumentManager.Presentation;

public class CreateDocumentDto
{
    public Guid Id { get; init; }
    public List<string> Tags { get; init; }
    public string Data { get; init; }
}