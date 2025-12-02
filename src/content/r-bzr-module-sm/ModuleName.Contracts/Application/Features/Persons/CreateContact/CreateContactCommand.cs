using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Application.Features.Persons.CreateContact;

public sealed record CreateContactCommand(PersonId PersonId, string Email) : ICommand<Result<ContactResult>>;