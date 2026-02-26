namespace ProjectName.Domain.PersonAggregate;

public sealed class PersonFirstName : ValueObject
{
    public const int MIN_LENGTH = 2;
    public const int MAX_LENGTH = 100;

    public string Value { get; private set; }

#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur autre que Null lors de la fermeture du constructeur. Envisagez d’ajouter le modificateur « required » ou de déclarer le champ comme pouvant accepter la valeur Null.
    private PersonFirstName()
    {
        // Required by EF Core for deserialization
    }
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur autre que Null lors de la fermeture du constructeur. Envisagez d’ajouter le modificateur « required » ou de déclarer le champ comme pouvant accepter la valeur Null.

    private PersonFirstName(string value)
    {
        Value = value;
    }

    public static Result<PersonFirstName> From(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Invalid(new ValidationError(nameof(value), "Le prénom ne peut pas être vide ou null."));

        value = value.Trim();

        if (value.Length <= MIN_LENGTH || value.Length >= MAX_LENGTH)
            return Result.Invalid(new ValidationError(nameof(value), $"Le prénom doit contenir entre {MIN_LENGTH} et {MAX_LENGTH} caractères."));

        return Result.Created(new PersonFirstName(value));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}