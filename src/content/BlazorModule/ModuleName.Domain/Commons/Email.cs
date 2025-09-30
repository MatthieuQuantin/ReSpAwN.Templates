namespace ModuleName.Domain.Commons;

public sealed class Email : ValueObject
{
    public string Value { get; private set; }

#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur autre que Null lors de la fermeture du constructeur. Envisagez d’ajouter le modificateur « required » ou de déclarer le champ comme pouvant accepter la valeur Null.
    private Email()
    {
        // Required by EF Core for deserialization
    }
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur autre que Null lors de la fermeture du constructeur. Envisagez d’ajouter le modificateur « required » ou de déclarer le champ comme pouvant accepter la valeur Null.

    private Email(string value)
    {
        Value = value;
    }

    public static Result<Email> From(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<Email>.Invalid(new ValidationError(nameof(value), "L'email ne peut pas être vide ou null."));

        value = value.Trim().ToLowerInvariant();

        //TODO : voir pour ajouter un controle de la qualité de l'email (regex, etc.)

        return Result.Created(new Email(value));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}