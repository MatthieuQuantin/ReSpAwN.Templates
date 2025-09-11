using ApplicationName.SharedKernel.Application.Persistence;
using ApplicationName.SharedKernel.Infrastructure.Persistence;

namespace ApplicationName.Infrastructure.Persistence;

internal sealed class EfUnitOfWork(ApplicationNameDbContext dbContext) : IUnitOfWork
{
    private readonly ApplicationNameDbContext _dbContext = dbContext;

    public async Task<IAppTransaction> BeginTransactionAsync(CancellationToken ct)
        => new EfTransaction(await _dbContext.Database.BeginTransactionAsync(ct));
}