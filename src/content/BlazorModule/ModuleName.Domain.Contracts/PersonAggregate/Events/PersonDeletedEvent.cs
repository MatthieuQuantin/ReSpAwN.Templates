namespace ModuleName.Domain.Contracts.PersonAggregate.Events;

public sealed class PersonDeletedEvent(Guid personId) : DomainEventBase
{
    public Guid PersonId { get; init; } = personId;
}