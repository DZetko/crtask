using Coderama.DocumentManager.Domain.Primitives;

namespace Coderama.DocumentManager.Domain.Entity;

public class Document: BaseEntity, IEquatable<Document>
{
    private readonly List<Tag> tags = [];
    private Document(){}
    private Document(
        Guid id,
        dynamic data) : base(id)
    {
        Data = data;
    }
    
    public static Document Create(Guid id, List<Tag> tags, string data)
    {
        ArgumentNullException.ThrowIfNull(id, nameof(id));
        ArgumentNullException.ThrowIfNull(tags, nameof(tags));
        ArgumentNullException.ThrowIfNull(data, nameof(data));
        var document = new Document(id, data);
        document.UpdateTags(tags);

        return document;
    }
    
    public void UpdateData(
        string data)
    {
        Data = data;
    }
    
    public void UpdateTags(
        ICollection<Tag> newTags)
    {
        tags.Clear();
        tags.AddRange(newTags);
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
    
    public IReadOnlyCollection<Tag> Tags => tags;
    public string Data { get; set; }
}