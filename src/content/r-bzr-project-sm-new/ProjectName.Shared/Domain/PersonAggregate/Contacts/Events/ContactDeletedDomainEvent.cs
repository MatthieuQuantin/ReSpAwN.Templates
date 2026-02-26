using ProjectName.SharedKernel.Domain;

namespace ProjectName.Domain.PersonAggregate.Contacts.Events;

public sealed class ContactDeletedDomainEvent(ContactId contactId) : DomainEventBase
{
    public ContactId ContactId { get; init; } = contactId;
}