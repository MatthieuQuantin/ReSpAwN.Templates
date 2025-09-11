using ApplicationName.Domain.Contracts.SampleAggregate.Events;

namespace ApplicationName.Domain.SampleAggregate;

public sealed class Sample : EntityBase<SampleId>, IAggregateRoot
{
    private Sample()
    {
        Id = SampleId.From(Guid.NewGuid());

        var @event = new SampleCreatedEvent(Id.Value);
        base.RegisterDomainEvent(@event);
    }

    public static Result<Sample> Create()
    {
        return Result.Created(new Sample());
    }
}