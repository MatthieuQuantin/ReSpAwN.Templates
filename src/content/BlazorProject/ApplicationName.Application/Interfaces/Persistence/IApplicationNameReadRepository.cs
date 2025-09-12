namespace ApplicationName.Application.Interfaces.Persistence;

public interface IApplicationNameReadRepository<T> : IReadRepository<T> where T : class, IAggregateRoot
{ }