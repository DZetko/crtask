namespace Coderama.DocumentManager.Presentation.Dto;

public record DocumentResponse
{
    public Guid Id { get; init; }
    public string Data { get; init; }
    public List<string> Tags { get; init; }
}