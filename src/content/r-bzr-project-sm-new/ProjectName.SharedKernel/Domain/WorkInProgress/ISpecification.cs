namespace ProjectName.SharedKernel.Domain.WorkInProgress;

/// <summary>
/// Interface représentant une spécification.
/// </summary>
public interface ISpecification
{
    /// <summary>
    /// Détermine si l'entité satisfait la spécification.
    /// </summary>
    /// <typeparam name="T">Type de l'entité à vérifier.</typeparam>
    /// <param name="entity">L'entité à vérifier.</param>
    /// <returns>True si l'entité satisfait la spécification, sinon false.</returns>
    bool IsSatisfiedBy<T>(T entity);
}
