using FluentValidation;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.User;

internal class ChangeAddressCommandValidator : AbstractValidator<ChangeAddressCommand>
{
    public ChangeAddressCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty();
        RuleFor(x => x.Phone)
            .NotNull()
            .NotEmpty()
            .MaximumLength(30);
        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(64);
        RuleFor(x => x.Street)
            .NotNull()
            .NotEmpty()
            .MaximumLength(64);
        RuleFor(x => x.PostalCode)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.City)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50);
    }
}