namespace ProjectName.SharedKernel.Domain.WorkInProgress;

/// <summary>
/// Interface pour les entités versionnables.
/// </summary>
/// <typeparam name="TId">Type de l'identifiant.</typeparam>
public interface IVersionable<TId> : IAuditable, IUserTrackable
    where TId : struct, IEquatable<TId>
{
    /// <summary>
    /// Identifiant de l'entité.
    /// </summary>
    TId Id { get; }
}
