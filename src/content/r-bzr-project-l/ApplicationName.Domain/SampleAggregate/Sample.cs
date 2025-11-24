using ApplicationName.Domain.Contracts.SampleAggregate.Events;

namespace ApplicationName.Domain.SampleAggregate;

public sealed class Sample : EntityBase<SampleId>, IAggregateRoot
{
    public string Name { get; private set; }

#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur autre que Null lors de la fermeture du constructeur. Envisagez d’ajouter le modificateur « required » ou de déclarer le champ comme pouvant accepter la valeur Null.
    /// <summary>
    /// Constructeur privé pour EF Core
    /// </summary>
    private Sample()
    {
        // Required by EF Core for deserialization
    }
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur autre que Null lors de la fermeture du constructeur. Envisagez d’ajouter le modificateur « required » ou de déclarer le champ comme pouvant accepter la valeur Null.

    private Sample(string name)
    {
        Id = SampleId.From(Guid.NewGuid());
        Name = name;

        var @event = new SampleCreatedDomainEvent(Id);
        base.RegisterDomainEvent(@event);
    }

    public static Result<Sample> Create(string name)
    {
        List<ValidationError> validationErrors = [];

        if (string.IsNullOrWhiteSpace(name))
            validationErrors.Add(new ValidationError(nameof(name), "Name cannot be empty"));

        if (validationErrors.Count != 0)
            return Result<Sample>.Invalid(validationErrors);

        return Result.Created(new Sample(name.Trim()));
    }
}