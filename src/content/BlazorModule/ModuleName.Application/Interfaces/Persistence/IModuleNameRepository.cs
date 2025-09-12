namespace ModuleName.Application.Interfaces.Persistence;

public interface IModuleNameRepository<T> : IRepository<T> where T : class, IAggregateRoot
{ }