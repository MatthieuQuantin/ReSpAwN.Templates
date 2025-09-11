namespace ModuleName.Application.Features.Persons.UpdatePersonContact;

internal sealed class UpdatePersonContactCommandValidator : AbstractValidator<UpdatePersonContactCommand>
{
    public UpdatePersonContactCommandValidator()
    {
        RuleFor(x => x.PersonId).NotEmpty();
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}