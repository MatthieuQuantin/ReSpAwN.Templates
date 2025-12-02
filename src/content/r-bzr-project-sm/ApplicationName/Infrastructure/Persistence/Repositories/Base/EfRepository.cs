using ApplicationName.Application.Persistence.Repositories;
using ApplicationName.SharedKernel.Application.Persistence;

namespace ApplicationName.Infrastructure.Persistence.Repositories.Base;

internal class EfRepository<T>(ApplicationNameDbContext dbContext) : ARepository<T>(dbContext), IApplicationNameRepository<T>, IApplicationNameReadRepository<T> where T : class, IAggregateRoot
{ }