using Ardalis.SharedKernel;
using Ardalis.Specification.EntityFrameworkCore;

namespace ApplicationName.Infrastructure.Persistence;

internal class EfRepository<T>(ApplicationNameDbContext dbContext) : RepositoryBase<T>(dbContext), IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{ }