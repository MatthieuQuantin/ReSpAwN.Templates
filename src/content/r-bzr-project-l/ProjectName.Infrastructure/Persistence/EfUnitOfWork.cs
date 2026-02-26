using ProjectName.SharedKernel.Application.Persistence;
using ProjectName.SharedKernel.Infrastructure.Persistence;

namespace ProjectName.Infrastructure.Persistence;

internal sealed class EfUnitOfWork(ProjectNameDbContext dbContext) : IUnitOfWork
{
    private readonly ProjectNameDbContext _dbContext = dbContext;

    public async Task<IAppTransaction> BeginTransactionAsync(CancellationToken ct)
        => new EfTransaction(await _dbContext.Database.BeginTransactionAsync(ct));
}