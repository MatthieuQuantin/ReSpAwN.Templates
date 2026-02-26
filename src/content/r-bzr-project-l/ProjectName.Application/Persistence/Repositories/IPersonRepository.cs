using ProjectName.Domain.PersonAggregate;

namespace ProjectName.Application.Persistence.Repositories;

public interface IPersonRepository : IProjectNameRepository<Person>, IProjectNameReadRepository<Person>
{
    /// <summary>
    /// Sample method to demonstrate a custom query.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<Person>> GetPersonsWithMoreThan2Contacts(CancellationToken cancellationToken = default);
}