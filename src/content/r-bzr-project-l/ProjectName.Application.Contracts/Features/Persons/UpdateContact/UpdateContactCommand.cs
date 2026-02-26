using ProjectName.Domain.PersonAggregate;

namespace ProjectName.Application.Features.Persons.UpdateContact;

public sealed record UpdateContactCommand(PersonId PersonId, ContactId ContactId, string Email) : ICommand<Result>;