namespace ApplicationName.SharedKernel.Application.Persistence;

public interface IUnitOfWork
{
    Task<IAppTransaction> BeginTransactionAsync(CancellationToken ct = default);
}
