namespace ModuleName.Domain.Contracts.PersonAggregate.Events;

public sealed class ContactUpdatedEvent(Guid contactId, string oldEmail, string newEmail) : DomainEventBase
{
    public Guid ContactId { get; init; } = contactId;

    public string OldEmail { get; init; } = oldEmail;

    public string NewEmail { get; init; } = newEmail;
}