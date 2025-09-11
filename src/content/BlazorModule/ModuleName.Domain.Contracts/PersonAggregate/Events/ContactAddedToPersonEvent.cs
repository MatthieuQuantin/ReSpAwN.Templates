namespace ModuleName.Domain.Contracts.PersonAggregate.Events;

public sealed class ContactAddedToPersonEvent(Guid personId, Guid contactId) : DomainEventBase
{
    public Guid PersonId { get; init; } = personId;

    public Guid ContactId { get; init; } = contactId;
}