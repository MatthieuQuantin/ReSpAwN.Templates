using ProjectName.Domain.Commons;

namespace ProjectName.Domain.PersonAggregate;

public sealed class Contact : EntityBase<ContactId>
{
    public Email Email { get; private set; }

#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur autre que Null lors de la fermeture du constructeur. Envisagez d’ajouter le modificateur « required » ou de déclarer le champ comme pouvant accepter la valeur Null.
    private Contact()
    {
        // Required by EF Core for deserialization
    }
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur autre que Null lors de la fermeture du constructeur. Envisagez d’ajouter le modificateur « required » ou de déclarer le champ comme pouvant accepter la valeur Null.

    private Contact(Email email)
    {
        Id = ContactId.From(Guid.NewGuid());
        Email = email;

        var @event = new ContactCreatedDomainEvent(Id);
        base.RegisterDomainEvent(@event);
    }

    public static Result<Contact> Create(string email)
    {
        var emailResult = Email.From(email);
        if (emailResult.IsInvalid())
            return Result.Invalid(emailResult.ValidationErrors);

        return Result.Created(new Contact(emailResult.Value));
    }

    internal Result Update(string email)
    {
        var emailResult = Email.From(email);
        if (emailResult.IsInvalid())
            return Result.Invalid(emailResult.ValidationErrors);

        var newEmail = emailResult.Value;

        if (Email.Equals(newEmail))
            return Result.Success();

        var oldEmail = Email;
        Email = newEmail;

        var @event = new ContactUpdatedDomainEvent(Id);
        base.RegisterDomainEvent(@event);

        return Result.Success();
    }
}