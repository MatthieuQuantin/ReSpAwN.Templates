using ApplicationName.Domain.SampleAggregate.Events;

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
        if (string.IsNullOrWhiteSpace(name))
            return Result.Invalid(new ValidationError(nameof(name), "Name cannot be empty"));

        return Result.Created(new Sample(name.Trim()));
    }
}