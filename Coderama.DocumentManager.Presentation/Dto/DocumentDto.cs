namespace Coderama.DocumentManager.Presentation.Dto;

public record DocumentDto
{
    public Guid Id { get; set; }
    public string Data { get; set; }
    public List<string> Tags { get; set; }
}