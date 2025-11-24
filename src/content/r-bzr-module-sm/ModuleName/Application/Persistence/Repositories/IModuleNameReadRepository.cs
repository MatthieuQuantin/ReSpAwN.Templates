namespace ModuleName.Application.Persistence.Repositories;

public interface IModuleNameReadRepository<T> : IReadRepository<T> where T : class, IAggregateRoot
{ }