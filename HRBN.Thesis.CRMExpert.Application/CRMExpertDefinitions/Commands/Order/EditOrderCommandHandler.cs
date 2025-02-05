﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Order
{
    public sealed class EditOrderCommandHandler : CommandHandlerBase<EditOrderCommand>
    {
        public EditOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<Result> HandleAsync(EditOrderCommand command)
        {
            var validationResult = await new EditOrderCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var order = await _unitOfWork.OrdersRepository.GetAsync(command.Id);
            if (order == null)
            {
                return Result.Fail("Order does not exist.");
            }

            _mapper.Map(command, order);

            order.ModDate = DateTime.Now;

            await _unitOfWork.OrdersRepository.UpdateAsync(order);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
