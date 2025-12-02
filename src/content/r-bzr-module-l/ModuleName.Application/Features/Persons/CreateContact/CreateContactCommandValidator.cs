namespace ModuleName.Application.Features.Persons.CreateContact;

internal sealed class CreateContactCommandValidator : AbstractValidator<CreateContactCommand>
{
    public CreateContactCommandValidator()
    {
        RuleFor(x => x.PersonId).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}