using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Application.Features.Persons.CreatePerson;

internal sealed class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
{
    public CreatePersonCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MinimumLength(PersonFirstName.MinLength).MaximumLength(PersonFirstName.MaxLength);
        RuleFor(x => x.LastName).NotEmpty().MinimumLength(PersonLastName.MinLength).MaximumLength(PersonLastName.MaxLength);
    }
}