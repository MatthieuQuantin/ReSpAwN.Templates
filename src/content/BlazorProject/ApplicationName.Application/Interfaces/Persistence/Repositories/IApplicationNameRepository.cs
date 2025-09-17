namespace ApplicationName.Application.Interfaces.Persistence.Repositories;

public interface IApplicationNameRepository<T> : IRepository<T> where T : class, IAggregateRoot
{ }