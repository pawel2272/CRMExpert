using System;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Todo
{
    public sealed class EditTodoCommandHandler : CommandHandlerBase<EditTodoCommand>
    {
        public EditTodoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<Result> HandleAsync(EditTodoCommand command)
        {
            var validationResult = await new EditTodoCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var todo = await _unitOfWork.TodosRepository.GetAsync(command.Id);
            if (todo == null)
            {
                return Result.Fail("Todo does not exist.");
            }

            _mapper.Map(command, todo);

            todo.ModDate = DateTime.Now;

            await _unitOfWork.TodosRepository.UpdateAsync(todo);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
