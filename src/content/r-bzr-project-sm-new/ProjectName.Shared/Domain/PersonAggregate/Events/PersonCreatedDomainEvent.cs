using ProjectName.SharedKernel.Domain;

namespace ProjectName.Domain.PersonAggregate.Events;

public sealed class PersonCreatedDomainEvent(PersonId personId) : DomainEventBase
{
    public PersonId PersonId { get; init; } = personId;
}