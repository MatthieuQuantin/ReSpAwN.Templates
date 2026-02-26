using ProjectName.SharedKernel.Domain;

namespace ProjectName.Domain.PersonAggregate;

public readonly record struct PersonFirstName : IValueObject<PersonFirstName, string>
{
    public const int MIN_LENGTH = 2;
    public const int MAX_LENGTH = 100;

    public string Value { get; }

    private PersonFirstName(string value)
    {
        Value = value;
    }

    public static Result<PersonFirstName> From(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Invalid(new ValidationError(nameof(value), "PersonFirstName.IsNullOrEmpty"));

        value = value.Trim();

        if (value.Length <= MIN_LENGTH || value.Length >= MAX_LENGTH)
            return Result.Invalid(new ValidationError(nameof(value), $"PersonFirstName.LengthOutOfRange"));

        return Result.Created(new PersonFirstName(value));
    }

    public static implicit operator PersonFirstName(string value)
        => From(value).Value;

    public static implicit operator string(PersonFirstName value)
        => value.Value;
}