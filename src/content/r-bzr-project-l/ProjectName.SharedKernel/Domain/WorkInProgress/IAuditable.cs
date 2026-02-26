namespace ProjectName.SharedKernel.Domain.WorkInProgress;

/// <summary>
/// Interface pour les entités auditées.
/// </summary>
public interface IAuditable
{
    /// <summary>
    /// Date de création de l'entité.
    /// </summary>
    DateTime CreatedAt { get; }

    /// <summary>
    /// Date de la dernière modification de l'entité.
    /// </summary>
    DateTime ModifiedAt { get; set; }
}
