using ProjectName.Application.Persistence.Repositories;
using ProjectName.Domain.PersonAggregate;
using ProjectName.Infrastructure.Persistence.Repositories.Base;

namespace ProjectName.Infrastructure.Persistence.Repositories;

internal sealed class PersonRepository(ProjectNameDbContext dbContext) : EfRepository<Person>(dbContext), IPersonRepository
{
    /// <inheritdoc />
    public Task<List<Person>> GetPersonsWithMoreThan2Contacts(CancellationToken cancellationToken = default)
    {
        return DbContext.Set<Person>()
            .Include(p => p.Contacts)
            .Where(p => p.Contacts.Count() > 2)
            .ToListAsync(cancellationToken);
    }
}