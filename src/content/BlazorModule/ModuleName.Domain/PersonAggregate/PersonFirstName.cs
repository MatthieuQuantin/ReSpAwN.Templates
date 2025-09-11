namespace ModuleName.Domain.PersonAggregate;

public sealed class PersonFirstName : ValueObject
{
    public const int MinLength = 2;
    public const int MaxLength = 100;

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

    public static Result<PersonFirstName> Create(string value)
    {
        var tmpValue = value?.Trim();

        if (string.IsNullOrWhiteSpace(tmpValue))
            return Result<PersonFirstName>.Invalid(new ValidationError("Le prénom ne peut pas être vide ou null."));

        if (tmpValue.Length < MinLength || tmpValue.Length > MaxLength)
            return Result<PersonFirstName>.Invalid(new ValidationError($"Le prénom doit contenir entre {MinLength} et {MaxLength} caractères."));

        return Result.Created(new PersonFirstName(tmpValue));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}