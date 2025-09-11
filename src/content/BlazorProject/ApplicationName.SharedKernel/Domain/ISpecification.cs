namespace ApplicationName.SharedKernel.Domain;

public interface ISpecification
{
    bool IsSatisfiedBy<T>(T entity);
}