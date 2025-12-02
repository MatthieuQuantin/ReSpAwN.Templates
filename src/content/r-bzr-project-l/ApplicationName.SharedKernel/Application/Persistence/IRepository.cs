using Ardalis.Specification;

namespace ApplicationName.SharedKernel.Application.Persistence;

public interface IRepository<T> : IReadRepository<T>, IRepositoryBase<T> where T : class, IAggregateRoot;