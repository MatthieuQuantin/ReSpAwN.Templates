using ProjectName.Domain.PersonAggregate.Contacts;
using ProjectName.SharedKernel.Domain;

namespace ProjectName.Domain.PersonAggregate.Events;

public sealed class ContactAddedToPersonDomainEvent(PersonId personId, ContactId contactId) : DomainEventBase
{
    public PersonId PersonId { get; init; } = personId;

    public ContactId ContactId { get; init; } = contactId;
}