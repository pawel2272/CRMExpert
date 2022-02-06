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

            var product = await _unitOfWork.ProductsRepository.GetAsync(order.ProductId);

            if (product.Count - order.Count < 0)
            {
                return Result.Fail($"Order count must be lower than or equal to count of product ({product.Count})!",
                    "Count");
            }

            product.Count -= order.Count;

            var customer = await _unitOfWork.CustomersRepository.GetCustomerByContactIdAsync(command.ContactId);
            var discount = await _unitOfWork.DiscountsRepository.GetProductDiscountAsync(command.ProductId, customer.Id);

            if (discount != null)
            {
                product.Price -= (product.Price * discount.DiscountVaule);
            }
            
            await _unitOfWork.ProductsRepository.UpdateAsync(product);
            
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}