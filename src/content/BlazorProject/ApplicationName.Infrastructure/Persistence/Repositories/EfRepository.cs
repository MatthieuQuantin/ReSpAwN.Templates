using ApplicationName.Application.Interfaces.Persistence.Repositories;

namespace ApplicationName.Infrastructure.Persistence.Repositories;

internal class EfRepository<T>(ApplicationNameDbContext dbContext) : RepositoryBase<T>(dbContext), IApplicationNameRepository<T>, IApplicationNameReadRepository<T> where T : class, IAggregateRoot
{ }