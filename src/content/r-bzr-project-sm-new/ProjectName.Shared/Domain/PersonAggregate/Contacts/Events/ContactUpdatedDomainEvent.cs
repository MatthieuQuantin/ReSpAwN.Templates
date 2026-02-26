using ProjectName.SharedKernel.Domain;

namespace ProjectName.Domain.PersonAggregate.Contacts.Events;

public sealed class ContactUpdatedDomainEvent(ContactId contactId) : DomainEventBase
{
    public ContactId ContactId { get; init; } = contactId;
}