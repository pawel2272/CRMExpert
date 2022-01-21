using FluentValidation;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Discount
{
    internal class SearchDiscountsQueryValidator : AbstractValidator<SearchDiscountsQuery>
    {
        public SearchDiscountsQueryValidator()
        {
            RuleFor(x => x)
                .Custom((value, context) =>
                {
                    if (value.PageNumber <= 0)
                    {
                        context.AddFailure("PageNumber", "Page number must be greater than 0.");
                    }

                    if (value.PageSize <= 0)
                    {
                        context.AddFailure("PageSize", "Page size must be greater than 0.");
                    }
                });
        }
    }
}
