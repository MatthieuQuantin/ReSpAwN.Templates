using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Application.Features.Persons.CreatePerson;

public sealed record PersonResult(PersonId Id, string FirstName, string LastName);