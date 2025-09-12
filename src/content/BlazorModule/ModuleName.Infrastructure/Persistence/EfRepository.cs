using Ardalis.Specification.EntityFrameworkCore;
using ModuleName.Application.Interfaces.Persistence;

namespace ModuleName.Infrastructure.Persistence;

internal class EfRepository<T>(ModuleNameDbContext dbContext) : RepositoryBase<T>(dbContext), IModuleNameRepository<T>, IModuleNameReadRepository<T> where T : class, IAggregateRoot
{ }