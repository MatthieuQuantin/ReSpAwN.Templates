using Ardalis.Specification;
using ProjectName.Domain.PersonAggregate;

namespace ProjectName.Application.Features.Persons.GetPersonByIdWithContacts.Specifications;

internal class PersonByIdWithContactsSpecification : SingleResultSpecification<Person, PersonResult>
{
    public PersonByIdWithContactsSpecification(PersonId id)
    {
        Query
            .Include(p => p.Contacts)
            .Where(p => p.Id == id)
            .Select(p => new PersonResult(
                p.Id,
                p.FirstName.Value,
                p.LastName.Value,
                p.Contacts.Select(c => new ContactResult(c.Id.Value, c.Email.Value)).ToList()));
    }
}