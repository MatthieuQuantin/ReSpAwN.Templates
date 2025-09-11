namespace ApplicationName.Domain.Contracts.SampleAggregate.Events;

public sealed class SampleCreatedEvent(Guid sampleId) : DomainEventBase
{
    public Guid SampleId { get; init; } = sampleId;
}