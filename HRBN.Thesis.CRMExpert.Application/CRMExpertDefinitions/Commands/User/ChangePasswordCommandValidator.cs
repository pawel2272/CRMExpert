using FluentValidation;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.User;

internal class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator(bool isPasswordValid)
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty();
        RuleFor(c => c.OldPassword)
            .Custom((oldPassword, context) =>
            {
                if (!isPasswordValid)
                {
                    context.AddFailure("OldPassword", "Invalid password!");
                }
            });
        RuleFor(c => c.RepeatPassword)
            .Custom((repeatPassword, context) =>
            {
                if (!context.InstanceToValidate.NewPassword.Equals(repeatPassword))
                {
                    context.AddFailure("RepeatPassword", "Passwords must be the same!");
                }
            });
    }
}