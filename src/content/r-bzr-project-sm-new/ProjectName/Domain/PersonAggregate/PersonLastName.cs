using ProjectName.SharedKernel.Domain;

namespace ProjectName.Domain.PersonAggregate;

public readonly record struct PersonLastName : IValueObject<PersonLastName, string>
{
    public const int MinLength = 2;
    public const int MaxLength = 100;

    public string Value { get; }

    private PersonLastName(string value)
    {
        Value = value;
    }

    public static Result<PersonLastName> From(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Invalid(new ValidationError(nameof(value), "PersonLastName.IsNullOrEmpty"));

        value = value.Trim();

        if (value.Length < MinLength || value.Length > MaxLength)
            return Result.Invalid(new ValidationError(nameof(value), "PersonLastName.LengthOutOfRange"));

        return Result.Created(new PersonLastName(value));
    }

    public static implicit operator PersonLastName(string value)
        => From(value).Value;

    public static implicit operator string(PersonLastName value)
        => value.Value;
}