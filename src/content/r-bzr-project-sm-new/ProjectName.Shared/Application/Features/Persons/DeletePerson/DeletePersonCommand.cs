using ProjectName.Domain.PersonAggregate;
using ProjectName.SharedKernel.Application;

namespace ProjectName.Application.Features.Persons.DeletePerson;

public sealed record DeletePersonCommand(PersonId PersonId) : ICommand<Result>;