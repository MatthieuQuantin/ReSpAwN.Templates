using ProjectName.Domain.PersonAggregate;

namespace ProjectName.Application.Features.Persons.ListPersons;

public sealed record PersonResult(PersonId Id, string FirstName, string LastName);