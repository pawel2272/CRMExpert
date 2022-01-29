using System;
using FluentValidation;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.User
{
    internal class EditUserCommandValidator : AbstractValidator<EditUserCommand>
    {
        public EditUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Username)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Gender)
                .NotNull()
                .NotEmpty()
                .MaximumLength(1);
            RuleFor(x => x.Password)
                // todo .MinimumLength(8)
                .MaximumLength(48);
            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30);
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
}