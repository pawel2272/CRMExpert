﻿using System;
using FluentValidation;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Discount
{
    internal class AddDiscountCommandValidator : AbstractValidator<AddDiscountCommand>
    {
        public AddDiscountCommandValidator()
        {
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
