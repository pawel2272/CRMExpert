using System;
using FluentValidation;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Customer
{
    internal class AddCustomerCommandValidator : AbstractValidator<AddCustomerCommand>
    {
        public AddCustomerCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(128);
            RuleFor(x => x.City)
                .NotNull()
                .NotEmpty()
                .MaximumLength(128);
            RuleFor(x => x.Street)
                .NotNull()
                .NotEmpty()
                .MaximumLength(128);
            RuleFor(x => x.PostalCode)
                .NotNull()
                .NotEmpty()
                .MaximumLength(128);
            RuleFor(x => x.TaxNo)
                .NotNull()
                .NotEmpty()
                .MaximumLength(128);
            RuleFor(x => x.Regon)
                .MaximumLength(128);
        }
    }
}
