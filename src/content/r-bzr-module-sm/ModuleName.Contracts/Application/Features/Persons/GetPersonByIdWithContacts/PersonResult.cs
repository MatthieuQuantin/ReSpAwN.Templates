using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Application.Features.Persons.GetPersonByIdWithContacts;

public sealed record PersonResult(PersonId Id, string FirstName, string LastName, IReadOnlyList<ContactResult> Contacts);