namespace ProjectName.Domain.PersonAggregate;

public sealed class PersonLastName : ValueObject
{
    public const int MinLength = 2;
    public const int MaxLength = 100;

    public string Value { get; private set; }

#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur autre que Null lors de la fermeture du constructeur. Envisagez d’ajouter le modificateur « required » ou de déclarer le champ comme pouvant accepter la valeur Null.
    private PersonLastName()
    {
        // Required by EF Core for deserialization
    }
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur autre que Null lors de la fermeture du constructeur. Envisagez d’ajouter le modificateur « required » ou de déclarer le champ comme pouvant accepter la valeur Null.

    private PersonLastName(string value)
    {
        Value = value;
    }

    public static Result<PersonLastName> From(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Invalid(new ValidationError("Le nom de famille ne peut pas être vide ou null."));

        value = value.Trim();

        if (value.Length < MinLength || value.Length > MaxLength)
            return Result.Invalid(new ValidationError($"Le nom de famille doit contenir entre {MinLength} et {MaxLength} caractères."));

        return Result.Created(new PersonLastName(value));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}