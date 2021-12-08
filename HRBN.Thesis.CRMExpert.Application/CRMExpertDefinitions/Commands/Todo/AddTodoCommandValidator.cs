using FluentValidation;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Todo
{
    internal class AddTodoCommandValidator : AbstractValidator<AddTodoCommand>
    {
        public AddTodoCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.Content)
                .MaximumLength(2048);
            RuleFor(x => x.ContactId)
                .NotEmpty();
        }
    }
}
