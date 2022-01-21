using System;
using FluentValidation;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Permission
{
    internal class AddPermissionCommandValidator : AbstractValidator<AddPermissionCommand>
    {
        public AddPermissionCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .NotEmpty()
                .NotEqual(Guid.Empty);
            RuleFor(x => x.RoleId)
                .NotNull()
                .NotEmpty()
                .NotEqual(Guid.Empty);
        }
    }
}
