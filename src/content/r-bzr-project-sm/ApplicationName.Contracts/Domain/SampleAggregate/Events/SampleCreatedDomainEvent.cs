namespace ApplicationName.Domain.SampleAggregate.Events;

public sealed class SampleCreatedDomainEvent(SampleId sampleId) : DomainEventBase
{
    public SampleId SampleId { get; init; } = sampleId;
}