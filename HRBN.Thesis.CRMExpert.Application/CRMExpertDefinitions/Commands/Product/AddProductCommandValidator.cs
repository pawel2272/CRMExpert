using System;
using FluentValidation;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Product
{
    internal class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidator()
        {
            RuleFor(x => x.Price)
                .NotNull()
                .NotEmpty()
                .NotEqual(0.0m);
            RuleFor(x => x.Name)
                .MaximumLength(128);
            RuleFor(x => x.Description)
                .MaximumLength(2048);
            RuleFor(x => x.Type)
                .MaximumLength(128);
            RuleFor(x => x.Count)
                .NotNull()
                .NotEmpty();
        }
    }
}
