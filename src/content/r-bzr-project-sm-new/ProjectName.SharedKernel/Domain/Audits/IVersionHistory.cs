namespace ProjectName.SharedKernel.Domain.Audits;

/// <summary>
/// Interface représentant une ligne de version (d'historique)
/// </summary>
/// <typeparam name="TId">Type de l'identifiant de la ligne de version (d'historique)</typeparam>
/// <typeparam name="TEntityId">Type de l'identifiant de l'entité concernée par cette ligne de version (d'historique)</typeparam>
public interface IVersionHistory<TId, TEntityId>
    where TId : struct, IEquatable<TId>
    where TEntityId : struct, IEquatable<TEntityId>
{
    /// <summary>
    /// Identifiant de la ligne de version (d'historique)
    /// </summary>
    TId Id { get; }

    /// <summary>
    /// Identifiant de l'entité concernée par cette ligne de version (d'historique)
    /// </summary>
    TEntityId EntityId { get; }

    /// <summary>
    /// Date et heure de début de validité de cette ligne de version (d'historique)
    /// </summary>
    DateTime ValidFrom { get; }

    /// <summary>
    /// Date et heure de fin de validité de cette ligne de version (d'historique)
    /// </summary>
    DateTime ValidTo { get; }

    /// <summary>
    /// Raison du changement ayant conduit à la création de cette ligne de version (d'historique)
    /// </summary>
    string? ChangeReason { get; }
}
