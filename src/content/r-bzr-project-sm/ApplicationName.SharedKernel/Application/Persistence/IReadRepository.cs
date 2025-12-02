using Ardalis.Specification;
using System.Linq.Expressions;

namespace ApplicationName.SharedKernel.Application.Persistence;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
    /// <summary>
    /// Returns a boolean indicating whether all elements of a sequence satisfy a condition in combination with a <paramref name="specification"/>.
    /// </summary>
    /// <param name="specification">The encapsulated query logic.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<bool> AllAsync(ISpecification<T> specification, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a boolean indicating whether all elements of a sequence satisfy a condition.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<bool> AllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
}