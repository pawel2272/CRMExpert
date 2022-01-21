using FluentValidation;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Permission
{
    internal class SearchPermissionsQueryValidator : AbstractValidator<SearchPermissionsQuery>
    {
        public SearchPermissionsQueryValidator()
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
