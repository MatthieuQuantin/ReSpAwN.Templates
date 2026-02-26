// Based on Ardalis.SharedKernel v5.0.0
// https://github.com/ardalis/Ardalis.SharedKernel/tree/5.0.0
namespace ProjectName.SharedKernel.Domain;

/// <summary>
/// Apply this marker interface only to aggregate root entities in your domain model
/// Your repository implementation can use constraints to ensure it only operates on aggregate roots
/// </summary>
public interface IAggregateRoot { }
