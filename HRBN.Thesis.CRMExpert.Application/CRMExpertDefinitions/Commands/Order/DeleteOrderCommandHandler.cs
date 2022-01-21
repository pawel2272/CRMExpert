using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Order
{
    public sealed class DeleteOrderCommandHandler : CommandHandlerBase<DeleteOrderCommand>
    {
        public DeleteOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<Result> HandleAsync(DeleteOrderCommand command)
        {
            var order = await _unitOfWork.OrdersRepository.GetAsync(command.Id);
            if (order == null)
            {
                return Result.Fail("Order does not exist.");
            }

            await _unitOfWork.OrdersRepository.DeleteAsync(order);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
