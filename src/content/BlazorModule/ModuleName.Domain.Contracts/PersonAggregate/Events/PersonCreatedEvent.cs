namespace ModuleName.Domain.Contracts.PersonAggregate.Events;

public sealed class PersonCreatedEvent(Guid personId) : DomainEventBase
{
    public Guid PersonId { get; init; } = personId;
}