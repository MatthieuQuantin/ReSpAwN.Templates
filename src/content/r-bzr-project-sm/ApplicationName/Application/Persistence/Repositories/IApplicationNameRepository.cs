namespace ApplicationName.Application.Persistence.Repositories;

public interface IApplicationNameRepository<T> : IRepository<T> where T : class, IAggregateRoot
{ }