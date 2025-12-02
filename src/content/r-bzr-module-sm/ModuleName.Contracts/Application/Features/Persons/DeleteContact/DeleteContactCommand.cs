using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Application.Features.Persons.DeleteContact;

public sealed record DeleteContactCommand(PersonId PersonId, ContactId ContactId) : ICommand<Result>;