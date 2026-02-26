using ProjectName.SharedKernel.Application.Persistence;
using ProjectName.SharedKernel.Domain;

namespace ProjectName.Application.Persistence.Repositories;

public interface IProjectNameReadRepository<T> : IReadRepository<T> where T : class, IAggregateRoot
{ }