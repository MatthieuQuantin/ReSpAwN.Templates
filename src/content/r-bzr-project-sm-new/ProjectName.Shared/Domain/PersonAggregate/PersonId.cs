using ProjectName.SharedKernel.Domain;

namespace ProjectName.Domain.PersonAggregate;

public readonly record struct PersonId: IValueObject<PersonId, Guid>
{
    public Guid Value { get; }

    private PersonId(Guid value)
    {
        Value = value;
    }

    public static Result<PersonId> From(Guid value)
    {
        if (value == Guid.Empty)
            return Result.Invalid(new ValidationError(nameof(value), "L'identifiant de personne ne peut pas être vide."));
        return Result.Created(new PersonId(value));
    }

    public static implicit operator PersonId(Guid value)
        => From(value).Value;

    public static implicit operator Guid(PersonId value)
        => value.Value;
}