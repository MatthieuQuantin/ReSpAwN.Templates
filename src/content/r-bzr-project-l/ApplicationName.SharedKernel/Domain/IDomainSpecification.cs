namespace ApplicationName.SharedKernel.Domain;

public interface IDomainSpecification
{
    bool IsSatisfiedBy<T>(T entity);
}