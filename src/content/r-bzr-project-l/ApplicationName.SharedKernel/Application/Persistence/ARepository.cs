using Microsoft.EntityFrameworkCore;
using Ardalis.Specification.EntityFrameworkCore;
using Ardalis.Specification;
using System.Linq.Expressions;

namespace ApplicationName.SharedKernel.Application.Persistence;

public abstract class ARepository<T>(DbContext dbContext) : RepositoryBase<T>(dbContext), IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    /// <inheritdoc/>
    public virtual async Task<bool> AllAsync(ISpecification<T> specification, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification, true).AllAsync(predicate, cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<bool> AllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<T>().AllAsync(predicate, cancellationToken);
    }
}