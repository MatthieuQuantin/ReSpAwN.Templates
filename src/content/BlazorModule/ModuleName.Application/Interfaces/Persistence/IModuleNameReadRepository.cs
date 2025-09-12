namespace ModuleName.Application.Interfaces.Persistence;

public interface IModuleNameReadRepository<T> : IReadRepository<T> where T : class, IAggregateRoot
{ }