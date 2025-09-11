using ApplicationName.SharedKernel.Application.Persistence;
using ApplicationName.SharedKernel.Infrastructure.Persistence;

namespace ModuleName.Infrastructure.Persistence;

internal sealed class EfUnitOfWork(ModuleNameDbContext dbContext) : IUnitOfWork
{
    private readonly ModuleNameDbContext _dbContext = dbContext;

    public async Task<IAppTransaction> BeginTransactionAsync(CancellationToken ct)
        => new EfTransaction(await _dbContext.Database.BeginTransactionAsync(ct));
}