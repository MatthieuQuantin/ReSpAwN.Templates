namespace ModuleName.Domain.PersonAggregate.Events;

public sealed class ContactUpdatedDomainEvent(ContactId contactId) : DomainEventBase
{
    public ContactId ContactId { get; init; } = contactId;
}