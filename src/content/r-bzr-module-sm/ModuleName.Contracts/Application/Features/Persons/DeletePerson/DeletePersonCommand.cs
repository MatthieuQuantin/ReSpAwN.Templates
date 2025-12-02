using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Application.Features.Persons.DeletePerson;

public sealed record DeletePersonCommand(PersonId PersonId) : ICommand<Result>;