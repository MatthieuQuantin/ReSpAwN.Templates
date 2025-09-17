using ModuleName.Application.Interfaces.Persistence.Repositories;
using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Infrastructure.Persistence.Repositories;

internal sealed class PersonRepository(ModuleNameDbContext dbContext) : EfRepository<Person>(dbContext), IPersonRepository
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