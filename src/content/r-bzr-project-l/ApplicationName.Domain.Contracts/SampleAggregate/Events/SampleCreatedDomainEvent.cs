using ApplicationName.Domain.SampleAggregate;

namespace ApplicationName.Domain.Contracts.SampleAggregate.Events;

public sealed class SampleCreatedDomainEvent(SampleId sampleId) : DomainEventBase
{
    public SampleId SampleId { get; init; } = sampleId;
}