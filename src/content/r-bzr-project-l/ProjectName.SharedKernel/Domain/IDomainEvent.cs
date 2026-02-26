// Based on Ardalis.SharedKernel v5.0.0
// https://github.com/ardalis/Ardalis.SharedKernel/tree/5.0.0
using MediatR;

namespace ProjectName.SharedKernel.Domain;

public interface IDomainEvent : INotification
{
    DateTime DateOccurred { get; }
}
