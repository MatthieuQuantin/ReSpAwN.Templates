using ModuleName.Application.Interfaces.Persistence.Repositories;

namespace ModuleName.Infrastructure.Persistence.Repositories;

internal class EfRepository<T>(ModuleNameDbContext dbContext) : RepositoryBase<T>(dbContext), IModuleNameRepository<T>, IModuleNameReadRepository<T> where T : class, IAggregateRoot
{ }