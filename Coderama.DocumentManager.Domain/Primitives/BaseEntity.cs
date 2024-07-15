namespace Coderama.DocumentManager.Domain.Primitives;

public class BaseEntity: IEquatable<BaseEntity>
{
    protected BaseEntity(Guid id)
    {
        Id = id;  
    } 
    
    protected BaseEntity()
    {
    }

    public Guid Id { get; private init; }

    public bool Equals(
        BaseEntity? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id.Equals(other.Id);
    }

    public override bool Equals(
        object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((BaseEntity) obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}