namespace ModuleName.Domain.Contracts.PersonAggregate.Events;

public sealed class PersonUpdatedEvent(Guid personId, string oldFirstName, string newFirstName, string oldLastName, string newLastName) : DomainEventBase
{
    public Guid PersonId { get; init; } = personId;

    public string OldFIrstName { get; init; } = oldFirstName;

    public string NewFirstName { get; init; } = newFirstName;

    public string OldLastName { get; init; } = oldLastName;

    public string NewLastName { get; init; } = newLastName;
}