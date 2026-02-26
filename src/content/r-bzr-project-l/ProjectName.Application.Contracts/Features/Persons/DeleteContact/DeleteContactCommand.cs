using ProjectName.Domain.PersonAggregate;

namespace ProjectName.Application.Features.Persons.DeleteContact;

public sealed record DeleteContactCommand(PersonId PersonId, ContactId ContactId) : ICommand<Result>;