// Based on Ardalis.SharedKernel v5.0.0
// https://github.com/ardalis/Ardalis.SharedKernel/tree/5.0.0
using MediatR;
using ProjectName.SharedKernel.Domain;

namespace ProjectName.SharedKernel.Application;

public interface IDomainEventHandler<T> : INotificationHandler<T> where T : IDomainEvent
{ }
