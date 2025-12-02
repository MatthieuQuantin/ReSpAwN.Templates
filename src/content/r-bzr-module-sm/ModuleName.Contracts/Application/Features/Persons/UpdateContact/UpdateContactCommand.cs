using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Application.Features.Persons.UpdateContact;

public sealed record UpdateContactCommand(PersonId PersonId, ContactId ContactId, string Email) : ICommand<Result>;