using System.Data;
using System.Text.Json;
using Coderama.DocumentManager.Domain;
using Coderama.DocumentManager.Domain.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Coderama.DocumentManager.Persistence.Repository;

public sealed class DocumentRepository : IDocumentRepository
{
    private readonly IConfiguration _configuration;
    private readonly DocumentManagerDbContext _dbContext;
    private readonly ILogger<DocumentRepository> _logger;

    public DocumentRepository(IConfiguration configuration, DocumentManagerDbContext dbContext, ILogger<DocumentRepository> logger)
    {
        _configuration = configuration;
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task<Document?> GetDocumentByIdASync(Guid id)
    {
        // Avoiding EF here to minimize the performance penalty of using an ORM
        // It must be evaluated, though, if the performance gains outweigh the lack of strong typing of the below
        // the below plain SQL query and also the proneness to error if the persistence or DB models change

        try
        {
            var connectionString = _configuration.GetConnectionString("DocumentManagerStore");
            await using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            await using var getDocumentByIdCommand = new SqlCommand
            {
                CommandText = "SELECT * FROM Documents WHERE id = @id",
                CommandType = CommandType.Text,
                Connection = connection
            };
            getDocumentByIdCommand.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;
            await using var reader = await getDocumentByIdCommand.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                var tags = reader[1];
                var data = reader[2].ToString();
                List<string> parsedTags = new();
                if (tags != null)
                {
                    parsedTags = JsonSerializer.Deserialize<List<string>>(tags.ToString());
                }
                return Document.Create(id, parsedTags, data);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
        }

        return null;
    }

    public async Task CreateDocumentAsync(
        Document document)
    {
        await _dbContext.Documents.AddAsync(document);
    }

    public async Task UpdateDocumentAsync(
        Document document)
    {
        if (!_dbContext.Documents.Contains(document))
        {
            throw new Exception($"Could not find a document with: {document.Id}");
        }
        _dbContext.Documents.Update(document);
    }
}