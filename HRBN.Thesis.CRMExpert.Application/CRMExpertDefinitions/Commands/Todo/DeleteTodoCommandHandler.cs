using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Application.Core;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Todo
{
    public sealed class DeleteTodoCommandHandler : ICommandHandler<DeleteTodoCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTodoCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(DeleteTodoCommand command)
        {
            var todo = await _unitOfWork.TodosRepository.GetAsync(command.Id);
            if (todo == null)
            {
                return Result.Fail("Todo does not exist.");
            }

            await _unitOfWork.TodosRepository.DeleteAsync(todo);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
