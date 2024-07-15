using System.Text.Json;
using Coderama.DocumentManager.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Document = Coderama.DocumentManager.Domain.Entity.Document;

namespace Coderama.DocumentManager.Persistence;

public class DocumentManagerDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Document>()
            .HasMany(d => d.Tags)
            .WithOne(d => d.Document);
    }

    public DbSet<Document> Documents { get; internal set; }
    public DbSet<Tag> Tags { get; internal set; }
}