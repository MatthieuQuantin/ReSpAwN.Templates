namespace ApplicationName.Application.Interfaces.Persistence;

public interface IApplicationNameRepository<T> : IRepository<T> where T : class, IAggregateRoot
{ }