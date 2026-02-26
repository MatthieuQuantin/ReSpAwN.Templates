// Based on Ardalis.SharedKernel v5.0.0
// https://github.com/ardalis/Ardalis.SharedKernel/tree/5.0.0
// and adapted to MediatR for domain event dispatching
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectName.SharedKernel.Domain;

namespace ProjectName.SharedKernel.Infrastructure.Persistence;

public class MediatRDomainEventDispatcher(IMediator mediator, ILogger<MediatRDomainEventDispatcher> logger) : IDomainEventDispatcher
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<MediatRDomainEventDispatcher> _logger = logger;

    public async Task DispatchAndClearEvents(IEnumerable<IHasDomainEvents> entitiesWithEvents)
    {
        foreach (IHasDomainEvents entity in entitiesWithEvents)
        {
            if (entity is IHasDomainEvents hasDomainEvents)
            {
                IDomainEvent[] events = [.. hasDomainEvents.DomainEvents];
                hasDomainEvents.ClearDomainEvents();

                foreach (var domainEvent in events)
                    await _mediator.Publish(domainEvent).ConfigureAwait(false);
            }
            else
            {
                _logger.LogError(
                    "Entity of type {EntityType} does not inherit from {BaseType}. Unable to clear domain events.",
                    entity.GetType().Name,
                    nameof(IHasDomainEvents));
            }
        }
    }
}
