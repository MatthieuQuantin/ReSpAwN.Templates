namespace ModuleName.Domain.Contracts.PersonAggregate.Events;

public sealed class ContactDeletedEvent(Guid contactId) : DomainEventBase
{
    public Guid ContactId { get; init; } = contactId;
}