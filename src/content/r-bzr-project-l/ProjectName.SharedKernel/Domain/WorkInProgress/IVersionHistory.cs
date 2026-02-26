namespace ProjectName.SharedKernel.Domain.WorkInProgress;

/// <summary>
/// Interface représentant une ligne de version (d'historique)
/// </summary>
/// <typeparam name="TId">Type de l'identifiant de la ligne de version (d'historique)</typeparam>
public interface IVersionHistory<TId>
    where TId : struct, IEquatable<TId>
{
    /// <summary>
    /// Identifiant de la ligne de version (d'historique)
    /// </summary>
    TId Id { get; }

    /// <summary>
    ///
    /// </summary>
    DateTime ValidFrom { get; }

    /// <summary>
    ///
    /// </summary>
    DateTime ValidTo { get; }

    /// <summary>
    ///
    /// </summary>
    string? ChangeReason { get; }
}
