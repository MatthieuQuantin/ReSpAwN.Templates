using ProjectName.Domain.PersonAggregate;

namespace ProjectName.Application.Features.Persons.GetPersonByIdWithContacts;

public sealed record GetPersonByIdWithContactsQuery(PersonId PersonId) : IQuery<Result<PersonResult>>;