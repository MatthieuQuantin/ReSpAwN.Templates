using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Application.Features.Persons.GetPersonByIdWithContacts;

public sealed record GetPersonByIdWithContactsQuery(PersonId PersonId) : IQuery<Result<PersonResult>>;