using ProjectName.SharedKernel.Application.Persistence;
using ProjectName.SharedKernel.Domain;

namespace ProjectName.Application.Persistence.Repositories;

public interface IProjectNameRepository<T> : IRepository<T> where T : class, IAggregateRoot
{ }