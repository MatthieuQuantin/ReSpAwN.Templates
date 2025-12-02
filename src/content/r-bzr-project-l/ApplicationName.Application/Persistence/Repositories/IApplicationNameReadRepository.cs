namespace ApplicationName.Application.Persistence.Repositories;

public interface IApplicationNameReadRepository<T> : IReadRepository<T> where T : class, IAggregateRoot
{ }