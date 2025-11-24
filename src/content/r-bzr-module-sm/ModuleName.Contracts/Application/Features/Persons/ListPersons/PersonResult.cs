using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Application.Features.Persons.ListPersons;

public sealed record PersonResult(PersonId Id, string FirstName, string LastName);