using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Application.Interfaces.Persistence;

public interface IPersonRepository : IRepository<Person>, IReadRepository<Person>
{
    /// <summary>
    /// Sample method to demonstrate a custom query.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<Person>> GetPersonsWithMoreThan2Contacts(CancellationToken cancellationToken = default);
}