namespace ModuleName.Domain.Contracts.PersonAggregate.Events;

public sealed class ContactCreatedEvent(Guid contactId) : DomainEventBase
{
    public Guid ContactId { get; init; } = contactId;
}