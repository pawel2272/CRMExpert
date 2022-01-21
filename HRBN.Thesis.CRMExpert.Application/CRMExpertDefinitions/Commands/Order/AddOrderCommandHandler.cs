using System;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Order
{
    public sealed class AddOrderCommandHandler : CommandHandlerBase<AddOrderCommand>
    {
        public AddOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<Result> HandleAsync(AddOrderCommand command)
        {
            var validationResult = await new AddOrderCommandValidator().ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var order = _mapper.Map<Domain.Core.Entities.Order>(command);
            order.Id = Guid.NewGuid();
            order.ModDate = DateTime.Now;
            order.CreDate = DateTime.Now;
            await _unitOfWork.OrdersRepository.AddAsync(order);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
