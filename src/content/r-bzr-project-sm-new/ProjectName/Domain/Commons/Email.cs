using ProjectName.SharedKernel.Domain;

namespace ProjectName.Domain.Commons;

public readonly record struct Email : IValueObject<Email, string>
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Result<Email> From(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Invalid(new ValidationError(nameof(value), "Email.IsNullOrEmpty"));

        value = Normalize(value);

        //TODO : voir pour ajouter un controle de la qualité de l'email (regex, etc.)

        return Result.Created(new Email(value));
    }

    public static string Normalize(string value)
    {
        return value.Trim().ToLowerInvariant();
    }

    public static implicit operator Email(string value)
        => From(value).Value;

    public static implicit operator string(Email value)
        => value.Value;
}