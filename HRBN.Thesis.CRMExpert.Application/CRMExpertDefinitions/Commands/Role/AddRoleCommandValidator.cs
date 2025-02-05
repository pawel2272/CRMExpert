﻿using FluentValidation;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Role
{
    internal class AddRoleCommandValidator : AbstractValidator<AddRoleCommand>
    {
        public AddRoleCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
