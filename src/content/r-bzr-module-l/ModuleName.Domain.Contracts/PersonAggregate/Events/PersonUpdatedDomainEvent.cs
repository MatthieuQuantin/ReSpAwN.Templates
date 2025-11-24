namespace ModuleName.Domain.PersonAggregate.Events;

public sealed class PersonUpdatedDomainEvent(PersonId personId) : DomainEventBase
{
    public PersonId PersonId { get; init; } = personId;
}