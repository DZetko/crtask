using Coderama.DocumentManager.Domain.Primitives;

namespace Coderama.DocumentManager.Domain.Entity;

public class Tag: BaseEntity
{
    private Tag() {}

    public static Tag Create(string value)
    {
        return new Tag
        {
            Value = value
        };
    }
    
    public string Value { get; private set; } = null!;
    public Document Document { get; private set; }
}