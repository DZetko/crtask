using Coderama.DocumentManager.Domain.Repository;

namespace Coderama.DocumentManager.Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly DocumentManagerDbContext _dbContext;

    public UnitOfWork(DocumentManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}