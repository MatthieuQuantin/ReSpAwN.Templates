using ApplicationName.SharedKernel.Application.Persistence;
using ModuleName.Application.Interfaces.Persistence.Repositories;

namespace ModuleName.Infrastructure.Persistence.Repositories;

internal class EfRepository<T>(ModuleNameDbContext dbContext) : ARepository<T>(dbContext), IModuleNameRepository<T>, IModuleNameReadRepository<T> where T : class, IAggregateRoot
{ }