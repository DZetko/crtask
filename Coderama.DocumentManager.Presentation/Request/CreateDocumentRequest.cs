namespace Coderama.DocumentManager.Presentation;

public class CreateDocumentRequest
{
    public Guid Id { get; init; }
    public List<string> Tags { get; init; }
    public object Data { get; init; }
}