// Based on Ardalis.SharedKernel v5.0.0
// https://github.com/ardalis/Ardalis.SharedKernel/tree/5.0.0
namespace ProjectName.SharedKernel.Domain;

/// <summary>
/// A base type for domain events. Depends on Mediator INotification.
/// Includes DateOccurred which is set on creation.
/// </summary>
public abstract class DomainEventBase : IDomainEvent
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}
