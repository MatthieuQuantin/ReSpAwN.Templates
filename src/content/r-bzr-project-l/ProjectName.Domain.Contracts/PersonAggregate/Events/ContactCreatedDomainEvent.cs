namespace ProjectName.Domain.PersonAggregate.Events;

public sealed class ContactCreatedDomainEvent(ContactId contactId) : DomainEventBase
{
    public ContactId ContactId { get; init; } = contactId;
}