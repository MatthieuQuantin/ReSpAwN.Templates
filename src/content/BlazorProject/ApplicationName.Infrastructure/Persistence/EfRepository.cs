using ApplicationName.Application.Interfaces.Persistence;
using Ardalis.Specification.EntityFrameworkCore;

namespace ApplicationName.Infrastructure.Persistence;

internal class EfRepository<T>(ApplicationNameDbContext dbContext) : RepositoryBase<T>(dbContext), IApplicationNameRepository<T>, IApplicationNameReadRepository<T> where T : class, IAggregateRoot
{ }