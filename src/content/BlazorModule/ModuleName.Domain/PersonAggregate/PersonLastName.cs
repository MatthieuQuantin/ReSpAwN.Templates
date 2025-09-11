namespace ModuleName.Domain.PersonAggregate;

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

    public static Result<PersonLastName> Create(string value)
    {
        var tmpValue = value?.Trim();

        if (string.IsNullOrWhiteSpace(tmpValue))
            return Result<PersonLastName>.Invalid(new ValidationError("Le nom de famille ne peut pas être vide ou null."));

        if (tmpValue.Length < MinLength || tmpValue.Length > MaxLength)
            return Result<PersonLastName>.Invalid(new ValidationError($"Le nom de famille doit contenir entre {MinLength} et {MaxLength} caractères."));

        return Result.Created(new PersonLastName(tmpValue));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}