using ProjectName.Domain.PersonAggregate;

namespace ProjectName.Application.Features.Persons.DeletePerson;

public sealed record DeletePersonCommand(PersonId PersonId) : ICommand<Result>;