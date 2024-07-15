using Coderama.DocumentManager.Domain.Repository;

namespace Coderama.DocumentManager.Persistence;

public sealed class UnitOfWork(DocumentManagerDbContext dbContext) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}