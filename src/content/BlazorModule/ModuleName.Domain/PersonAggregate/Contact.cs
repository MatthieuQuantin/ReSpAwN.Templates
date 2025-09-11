using ModuleName.Domain.Contracts.PersonAggregate.Events;

namespace ModuleName.Domain.PersonAggregate;

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

        var @event = new ContactCreatedEvent(Id.Value);
        base.RegisterDomainEvent(@event);
    }

    public static Result<Contact> Create(Email email)
    {
        if (email is null)
            return Result<Contact>.Invalid(new ValidationError(nameof(email), "L'email du contact ne peut pas être vide ou null."));

        return Result.Created(new Contact(email));
    }

    internal Result Update(Email newEmail)
    {
        List<ValidationError> validationErrors = [];

        if (newEmail is null)
            validationErrors.Add(new ValidationError(nameof(newEmail), "Le nouvel email ne peut pas être null."));

        if (validationErrors.Count != 0)
            return Result.Invalid(validationErrors);

        if (Email.Equals(newEmail))
            return Result.Success();

        var oldEmail = Email;
        Email = newEmail;

        var @event = new ContactUpdatedEvent(Id.Value, oldEmail.Value, newEmail.Value);
        base.RegisterDomainEvent(@event);

        return Result.Success();
    }
}