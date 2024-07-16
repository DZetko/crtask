namespace Coderama.DocumentManager.Presentation;

public class UpdateDocumentRequest
{
    public List<string> Tags { get; init; }
    public object Data { get; init; }
}