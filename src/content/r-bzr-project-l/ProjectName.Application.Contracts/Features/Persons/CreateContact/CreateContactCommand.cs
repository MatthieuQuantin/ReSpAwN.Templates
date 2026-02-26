using ProjectName.Domain.PersonAggregate;

namespace ProjectName.Application.Features.Persons.CreateContact;

public sealed record CreateContactCommand(PersonId PersonId, string Email) : ICommand<Result<ContactResult>>;