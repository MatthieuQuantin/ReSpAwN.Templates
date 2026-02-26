namespace ProjectName.SharedKernel.Domain.Audits;

/// <summary>
/// Interface pour les entités traçables par utilisateur.
/// </summary>
/// <typeparam name="TUserId">Type de l'identifiant de l'utilisateur (par exemple, int, Guid, etc.)</typeparam>
public interface IUserTrackable<TUserId>
    where TUserId : struct, IEquatable<TUserId>
{
    /// <summary>
    /// Identifiant de l'utilisateur ayant créé l'entité.
    /// </summary>
    TUserId CreatedBy { get; }

    /// <summary>
    /// Identifiant de l'utilisateur ayant modifié l'entité pour la dernière fois.
    /// </summary>
    TUserId ModifiedBy { get; set; }
}
