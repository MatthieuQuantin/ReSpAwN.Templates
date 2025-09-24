namespace ApplicationName.Application.Interfaces.Persistence.Repositories;

public interface IApplicationNameReadRepository<T> : IReadRepository<T> where T : class, IAggregateRoot
{ }