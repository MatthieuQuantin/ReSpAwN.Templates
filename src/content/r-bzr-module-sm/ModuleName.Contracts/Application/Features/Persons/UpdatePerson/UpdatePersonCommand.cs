using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Application.Features.Persons.UpdatePerson;

public sealed record UpdatePersonCommand(PersonId PersonId, string FirstName, string LastName) : ICommand<Result>;