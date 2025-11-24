namespace ModuleName.Application.Persistence.Repositories;

public interface IModuleNameRepository<T> : IRepository<T> where T : class, IAggregateRoot
{ }