// Based on Ardalis.SharedKernel v5.0.0
// https://github.com/ardalis/Ardalis.SharedKernel/tree/5.0.0
namespace ProjectName.SharedKernel.Domain;

public interface IHasDomainEvents
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    void ClearDomainEvents();
}
