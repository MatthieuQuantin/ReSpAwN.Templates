using ProjectName.SharedKernel.Domain;

namespace ProjectName.Domain.PersonAggregate.Contacts;

public readonly record struct ContactId : IValueObject<ContactId, Guid>
{
    public Guid Value { get; }

    private ContactId(Guid value)
    {
        Value = value;
    }

    public static Result<ContactId> From(Guid value)
    {
        return Result.Created(new ContactId(value));
    }

    public static implicit operator ContactId(Guid value)
        => From(value).Value;

    public static implicit operator Guid(ContactId value)
        => value.Value;
}