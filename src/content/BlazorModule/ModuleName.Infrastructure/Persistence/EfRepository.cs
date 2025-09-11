using Ardalis.Specification.EntityFrameworkCore;

namespace ModuleName.Infrastructure.Persistence;

internal class EfRepository<T>(ModuleNameDbContext dbContext) : RepositoryBase<T>(dbContext), IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{ }