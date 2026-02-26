using ProjectName.Domain.PersonAggregate;
using ProjectName.SharedKernel.Application;

namespace ProjectName.Application.Features.Persons.CreateContact;

public sealed record CreateContactCommand(PersonId PersonId, string Email) : ICommand<Result<ContactResult>>;