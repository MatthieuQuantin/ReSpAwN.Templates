using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectName.SharedKernel.Application.Persistence;
using ProjectName.SharedKernel.Domain;
using System.Linq.Expressions;

namespace ProjectName.SharedKernel.Infrastructure.Persistence;

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