namespace Coderama.DocumentManager.Presentation;

public class CreateDocumentRequest
{
    public Guid Id { get; init; }
    public List<string> Tags { get; init; }
    public string Data { get; init; }
}