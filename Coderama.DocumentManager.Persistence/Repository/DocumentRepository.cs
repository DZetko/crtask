using System.Data;
using Coderama.DocumentManager.Domain;
using Coderama.DocumentManager.Domain.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Coderama.DocumentManager.Persistence.Repository;

public sealed class DocumentRepository(
    IConfiguration configuration,
    DocumentManagerDbContext dbContext,
    ILogger<DocumentRepository> logger)
    : IDocumentRepository
{
    public async Task<Document?> GetDocumentByIdASync(Guid id)
    {
        // Avoiding EF here to minimize the performance penalty of using an ORM
        // It must be evaluated, though, if the performance gains outweigh the lack of strong typing of the below
        // the below plain SQL query and also the proneness to error if the persistence or DB models change
        try
        {
            var connectionString = configuration.GetConnectionString("DocumentManagerStore");
            await using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            await using var getTagsByDocumentIdCommand = new SqlCommand();
            getTagsByDocumentIdCommand.CommandText = "SELECT Value FROM Tags WHERE DocumentId = @DocumentId";
            getTagsByDocumentIdCommand.CommandType = CommandType.Text;
            getTagsByDocumentIdCommand.Connection = connection;
            getTagsByDocumentIdCommand.Parameters.Add("@DocumentId", SqlDbType.UniqueIdentifier).Value = id;
            await using var tagsReader = await getTagsByDocumentIdCommand.ExecuteReaderAsync();
            List<Tag> tags = [];
            while (await tagsReader.ReadAsync())
            {
                var value = tagsReader[0];
                if (value == null) continue;
                tags.Add(Tag.Create(value.ToString()));
            }

            await using var getDocumentByIdCommand = new SqlCommand();
            getDocumentByIdCommand.CommandText = "SELECT * FROM Documents WHERE id = @id";
            getDocumentByIdCommand.CommandType = CommandType.Text;
            getDocumentByIdCommand.Connection = connection;
            getDocumentByIdCommand.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;
            await using var documentsReader = await getDocumentByIdCommand.ExecuteReaderAsync();

            if (await documentsReader.ReadAsync())
            {
                var data = documentsReader[1].ToString();
                return Document.Create(id, tags, data);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
        }

        return null;
    }

    public async Task CreateDocumentAsync(
        Document document)
    {
        await dbContext.Documents.AddAsync(document);
    }

    public Task UpdateDocumentAsync(
        Document document)
    {
        if (!dbContext.Documents.Contains(document))
        {
            throw new Exception($"Could not find a document with: {document.Id}");
        }
        dbContext.Documents.Update(document);
        return Task.CompletedTask;
    }
}