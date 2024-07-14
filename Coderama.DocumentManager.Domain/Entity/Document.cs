namespace Coderama.DocumentManager.Domain.Entity;

public class Document: IEquatable<Document>
{
    public Guid Id { get; set; }
    public List<string> Tags { get; set; }
    public string Data { get; set; }
    
    private Document(){}
    private Document(
        Guid id,
        List<string> tags,
        dynamic data) : this()
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(tags);
        ArgumentNullException.ThrowIfNull(data);

        Id = id;
        Tags = tags;
        Data = data;
    }

    public static Document Create(Guid id, List<string> tags, string data)
    {
        return new Document(id, tags, data);
    }

    public void Update(
        List<string> tags,
        string data)
    {
        Tags = tags;
        Data = data;
    }

    public bool Equals(
        Document? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id.Equals(other.Id) && Tags.Equals(other.Tags) && Data.Equals(other.Data);
    }

    public override bool Equals(
        object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Document) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Tags, Data);
    }
}