using ProjectName.Domain.PersonAggregate;
using ProjectName.SharedKernel.Application;

namespace ProjectName.Application.Features.Persons.UpdatePerson;

public sealed record UpdatePersonCommand(PersonId PersonId, string FirstName, string LastName) : ICommand<Result>;