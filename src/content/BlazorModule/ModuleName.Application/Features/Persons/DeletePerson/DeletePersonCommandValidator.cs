namespace ModuleName.Application.Features.Persons.DeletePerson;

internal sealed class DeletePersonCommandValidator : AbstractValidator<DeletePersonCommand>
{
    public DeletePersonCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}