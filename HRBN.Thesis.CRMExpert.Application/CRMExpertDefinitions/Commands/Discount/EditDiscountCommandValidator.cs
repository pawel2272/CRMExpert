using System;
using FluentValidation;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Discount
{
    internal class EditDiscountCommandValidator : AbstractValidator<EditDiscountCommand>
    {
        public EditDiscountCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .NotEqual(Guid.Empty);
            RuleFor(x => x.DiscountVaule)
                .GreaterThan(0m)
                .LessThanOrEqualTo(0.50m)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.ProductId)
                .NotNull()
                .NotEmpty()
                .NotEqual(Guid.Empty);
            RuleFor(x => x.CustomerId)
                .NotNull()
                .NotEmpty()
                .NotEqual(Guid.Empty);
        }
    }
}