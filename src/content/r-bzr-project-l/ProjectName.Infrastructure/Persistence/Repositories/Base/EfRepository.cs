using ProjectName.Application.Persistence.Repositories;
using ProjectName.SharedKernel.Domain;
using ProjectName.SharedKernel.Infrastructure.Persistence;

namespace ProjectName.Infrastructure.Persistence.Repositories.Base;

internal class EfRepository<T>(ProjectNameDbContext dbContext) : ARepository<T>(dbContext), IProjectNameRepository<T>, IProjectNameReadRepository<T> where T : class, IAggregateRoot
{ }