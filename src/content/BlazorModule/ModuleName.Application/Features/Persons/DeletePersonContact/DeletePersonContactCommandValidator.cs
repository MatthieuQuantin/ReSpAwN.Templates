namespace ModuleName.Application.Features.Persons.DeletePersonContact;

internal sealed class DeletePersonContactCommandValidator : AbstractValidator<DeletePersonContactCommand>
{
    public DeletePersonContactCommandValidator()
    {
        RuleFor(x => x.PersonId).NotEmpty();
        RuleFor(x => x.Id).NotEmpty();
    }
}