using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Document = Coderama.DocumentManager.Domain.Entity.Document;

namespace Coderama.DocumentManager.Persistence;

public class DocumentManagerDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Document>()
            .Property<List<string>>(e => e.Tags)
            .HasConversion(
                r => JsonSerializer.Serialize(r, JsonSerializerOptions.Default),
                s => JsonSerializer.Deserialize<List<string>>(s, JsonSerializerOptions.Default) ?? new());
    }

    public DbSet<Document> Documents { get; internal set; }
}