namespace ModuleName.Domain.PersonAggregate.Events;

public sealed class ContactDeletedDomainEvent(ContactId contactId) : DomainEventBase
{
    public ContactId ContactId { get; init; } = contactId;
}