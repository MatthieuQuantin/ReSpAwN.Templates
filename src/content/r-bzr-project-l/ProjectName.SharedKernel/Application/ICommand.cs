// Based on Ardalis.SharedKernel v3.0.1
// https://github.com/ardalis/Ardalis.SharedKernel/tree/3.0.1
using MediatR;

namespace ProjectName.SharedKernel.Application;

/// <summary>
/// Source: https://code-maze.com/cqrs-mediatr-fluentvalidation/
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
