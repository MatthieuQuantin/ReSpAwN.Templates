using ApplicationName.SharedKernel.Application.Persistence;
using Microsoft.EntityFrameworkCore.Storage;

namespace ApplicationName.SharedKernel.Infrastructure.Persistence;

public sealed class EfTransaction(IDbContextTransaction inner) : IAppTransaction
{
    private readonly IDbContextTransaction _transaction = inner;

    public Task CommitAsync(CancellationToken cancellationToken = default) => _transaction.CommitAsync(cancellationToken);
    public Task RollbackAsync(CancellationToken cancellationToken = default) => _transaction.RollbackAsync(cancellationToken);
    public Task CreateSavepointAsync(string name, CancellationToken cancellationToken = default) => _transaction.CreateSavepointAsync(name, cancellationToken);
    public Task RollbackToSavepointAsync(string name, CancellationToken cancellationToken = default) => _transaction.RollbackToSavepointAsync(name, cancellationToken);
    public Task ReleaseSavepointAsync(string name, CancellationToken cancellationToken = default) => _transaction.ReleaseSavepointAsync(name, cancellationToken);

    public ValueTask DisposeAsync() => _transaction.DisposeAsync();
}