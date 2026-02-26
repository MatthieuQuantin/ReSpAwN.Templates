namespace ProjectName.Domain.PersonAggregate.Events;

public sealed class PersonDeletedDomainEvent(PersonId personId) : DomainEventBase
{
    public PersonId PersonId { get; init; } = personId;
}