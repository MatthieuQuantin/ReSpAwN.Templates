using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Application.Features.Persons.UpdatePerson;

internal sealed class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
{
    public UpdatePersonCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty().MinimumLength(PersonFirstName.MinLength).MaximumLength(PersonFirstName.MaxLength);
        RuleFor(x => x.LastName).NotEmpty().MinimumLength(PersonLastName.MinLength).MaximumLength(PersonLastName.MaxLength);
    }
}