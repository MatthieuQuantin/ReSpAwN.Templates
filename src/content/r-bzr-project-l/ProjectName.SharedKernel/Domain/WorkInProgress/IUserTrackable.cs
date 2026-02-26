namespace ProjectName.SharedKernel.Domain.WorkInProgress;

/// <summary>
/// Interface pour les entités traçables par utilisateur.
/// </summary>
public interface IUserTrackable
{
    /// <summary>
    /// Identifiant de l'utilisateur ayant créé l'entité.
    /// </summary>
    Guid CreatedBy { get; }

    /// <summary>
    /// Identifiant de l'utilisateur ayant modifié l'entité pour la dernière fois.
    /// </summary>
    Guid ModifiedBy { get; set; }
}
