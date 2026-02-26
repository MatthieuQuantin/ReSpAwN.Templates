namespace ProjectName.SharedKernel.Domain.Audits;

/// <summary>
/// Interface pour les entités versionnables.
/// </summary>
/// <typeparam name="TId">Type de l'identifiant.</typeparam>
/// <typeparam name="TUserId">Type de l'identifiant de l'utilisateur (par exemple, int, Guid, etc.)</typeparam>
public interface IVersionable<TId, TUserId> : IAuditable, IUserTrackable<TUserId>
    where TId : struct, IEquatable<TId>
    where TUserId : struct, IEquatable<TUserId>
{
    /// <summary>
    /// Identifiant de l'entité.
    /// </summary>
    TId Id { get; }
}
