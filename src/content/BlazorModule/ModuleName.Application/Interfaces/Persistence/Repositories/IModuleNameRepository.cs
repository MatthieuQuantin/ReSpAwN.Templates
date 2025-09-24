namespace ModuleName.Application.Interfaces.Persistence.Repositories;

public interface IModuleNameRepository<T> : IRepository<T> where T : class, IAggregateRoot
{ }